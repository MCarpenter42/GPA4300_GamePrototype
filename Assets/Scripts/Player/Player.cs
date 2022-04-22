using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CoreFunctionality
{
    #region [ PARAMETERS ]

    [Header("Camera")]

    [SerializeField] PlayerCam playerCam;
    private bool cursorLocked = false;

    private float rotFactor;
    private float camPitch;
    private float camYaw;

    public bool invertPitch;
    private int pitchDir;

    private GameObject shadow;

    [Header("Movement")]

    [SerializeField] float maxSpeed = 3.0f;
    [SerializeField] float sprintFactor = 2.0f;
    [SerializeField] float crouchFactor = 0.6f;

    Rigidbody rb;
    private Vector3 moveFactors = new Vector3();
    private float defaultDrag;

    private bool isOnFloor = false;
    [SerializeField] float jumpStrength = 2.0f;

  //[Header("Crouching")]

    CapsuleCollider cldr;

    private Coroutine heightTransition;
    private float defaultHeight;
    private float crouchScale = 0.6f;

    [Header("Interaction")]

    private List<Interaction> interacts;
    [SerializeField] float interactRange = 3.0f;
    [SerializeField] float interactAngle = 10.0f;
    private bool canInteract = false;

    private Interaction intrClosest;
    private Interaction intrClosestPrev;

  //[Header("Inventory")]

    public Inventory Inventory { get; private set; }

    [Header("Audio")]

    public AudioClip clipCrouchLoop;
    public AudioClip clipWalkLoop;
    public AudioClip clipRunLoop;

    public AudioClip clipItemPickup;

    private AudioSource[] sources = new AudioSource[4];
    private float[] defaultPitch = new float[4];
    private float[] defaultVolume = new float[4];

    private AudioSource moveSFX;
    private AudioSource itemSFX;
    private AudioSource ambientSFX;
    private AudioSource environmentSFX;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        GetComponents();
    }
    
    void Start()
    {
        LockCursor(true);
        GetInteractions();
        HideInteractInds();
    }

    void Update()
    {
        LookControl();
        CrouchControl();
        MoveControl();
        MoveAudio();
        InteractCheck();
    }

    void FixedUpdate()
    {
        MovementPhysics();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void LookControl()
    {
        if (GameManager.isCursorLocked && !GameManager.state.isPaused)
        {
            camPitch += rotFactor * Input.GetAxis("Mouse Y") * (float)pitchDir;
            camYaw += rotFactor * Input.GetAxis("Mouse X");
        }

        // Clamp pitch
        camPitch = Mathf.Clamp(camPitch, -90.0f, 90.0f);

        // Wrap yaw
        while (camYaw < 0.0f)
        {
            camYaw += 360.0f;
        }
        while (camYaw >= 360.0f)
        {
            camYaw -= 360.0f;
        }

        // Set player object orientation
        transform.eulerAngles = new Vector3(0.0f, camYaw, 0.0f);

        Camera();
    }
    
    private void CrouchControl()
    {
        if (Input.GetKeyDown(controls.movement.crouch))
        {
            if (heightTransition != null)
            {
                StopCoroutine(heightTransition);
            }
            heightTransition = StartCoroutine(HeightTransition(defaultHeight * crouchScale));
        }
        else if (Input.GetKeyUp(controls.movement.crouch))
        {
            if (heightTransition != null)
            {
                StopCoroutine(heightTransition);
            }
            heightTransition = StartCoroutine(HeightTransition(defaultHeight));
        }
    }

    private void MoveControl()
    {
        float movMulti = 8.0f;
        if (Input.GetKey(controls.movement.crouch))
        {
            movMulti *= crouchFactor;
        }
        else if (Input.GetKey(controls.movement.sprint))
        {
            movMulti *= sprintFactor;
        }

        float fwdFactor = 0;
        float rhtFactor = 0;
        if (isOnFloor)
        {
            if (Input.GetKey(controls.movement.forward))
            {
                fwdFactor += maxSpeed * movMulti;
            }
            if (Input.GetKey(controls.movement.back))
            {
                fwdFactor -= maxSpeed * movMulti;
            }
            if (Input.GetKey(controls.movement.right))
            {
                rhtFactor += maxSpeed * movMulti;
            }
            if (Input.GetKey(controls.movement.left))
            {
                rhtFactor -= maxSpeed * movMulti;
            }
            moveFactors[2] = fwdFactor;
            moveFactors[0] = rhtFactor;

            if (Input.GetKeyDown(controls.movement.jump))
            {
                moveFactors[1] = jumpStrength;
            }
        }
        else if (!isOnFloor)
        {
            moveFactors[0] = 0.0f;
            moveFactors[1] = 0.0f;
            moveFactors[2] = 0.0f;
        }
    }

    private void InteractCheck()
    {
        intrClosest = GetClosestInteract();

        if (intrClosest != null)
        {
            if (intrClosest != intrClosestPrev && intrClosestPrev != null)
            {
                intrClosestPrev.ShowIndicator(false);
            }

            Vector3 disp = intrClosest.pos - transform.position;
            float dist = disp.magnitude;

            Vector3 camFacing = playerCam.transform.forward;
            Vector3 camDisp = intrClosest.pos - playerCam.transform.position;
            float lookDeviation = Vector3.Angle(camFacing, camDisp);

            if (dist <= interactRange && lookDeviation <= interactAngle)
            {
                canInteract = true;
                intrClosest.ShowIndicator(true);
            }
            else
            {
                canInteract = false;
                intrClosest.ShowIndicator(false);
            }

            if (canInteract && Input.GetKeyDown(controls.actions.interact))
            {
                intrClosest.InteractEvent();
            }

        }

        intrClosestPrev = intrClosest;
    }

    private void MovementPhysics()
    {
        Vector3 velocityFlat = rb.velocity;
        velocityFlat[1] = 0.0f;
        Vector3 relVel = transform.InverseTransformDirection(velocityFlat);
        Vector3 decelForce = new Vector3(0.0f, 0.0f, 0.0f);
        float decelFactor = 8.0f;

        if (isOnFloor)
        {
            if (moveFactors[2] == 0.0f && (relVel[2] > 0.05f || relVel[2] < -0.05f))
            {
                if (relVel[2] > 0.0f)
                {
                    decelForce -= transform.forward * maxSpeed * decelFactor;
                }
                else
                {
                    decelForce += transform.forward * maxSpeed * decelFactor;
                }
            }
            if (moveFactors[0] == 0.0f && (relVel[0] > 0.05f || relVel[0] < -0.05f))
            {
                if (relVel[0] > 0.0f)
                {
                    decelForce -= transform.right * maxSpeed * decelFactor;
                }
                else
                {
                    decelForce += transform.right * maxSpeed * decelFactor;
                }
            }
        }

        Vector3 lateralForce = transform.forward * moveFactors[2] + transform.right * moveFactors[0];
        lateralForce += decelForce;
        rb.AddForce(lateralForce);

        if (relVel.magnitude < 0.08f)
        {
            Vector3 newVel = new Vector3(0.0f, rb.velocity[1], 0.0f);
            rb.velocity = newVel;
        }

        if (moveFactors[1] > 0.0f)
        {
            rb.AddForce(new Vector3(0.0f, moveFactors[1], 0.0f), ForceMode.Impulse);
            isOnFloor = false;
        }

        CapSpeed();
    }

    private void CapSpeed()
    {
        float movMulti = 1.0f;
        if (Input.GetKey(controls.movement.crouch))
        {
            movMulti *= crouchFactor;
        }
        else if (Input.GetKey(controls.movement.sprint))
        {
            movMulti *= sprintFactor;
        }

        Vector3 flatVel = new Vector3(rb.velocity[0], 0.0f, rb.velocity[2]);
        if (flatVel.magnitude > maxSpeed * movMulti && isOnFloor)
        {
            Vector3 newVel = flatVel.normalized * maxSpeed * movMulti + new Vector3(0.0f, rb.velocity[1], 0.0f);
            rb.velocity = newVel;
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void MoveAudio()
    {
        if (Input.GetKey(controls.movement.crouch))
        {
            moveSFX.clip = clipCrouchLoop;
        }
        else if (Input.GetKey(controls.movement.sprint))
        {
            moveSFX.clip = clipRunLoop;
        }
        else
        {
            moveSFX.clip = clipWalkLoop;
        }

        if (isOnFloor)
        {
            if (rb.velocity.magnitude < 0.2f)
            {
                moveSFX.Stop();
            }
            else if (!moveSFX.isPlaying)
            {
                moveSFX.Play();
            }
        }
        else
        {
            moveSFX.Stop();
        }
    }

    public void PlayClip(AudioSources source, AudioClip clip)
    {
        AudioSource useSource = sources[(int)source];
        useSource.pitch = defaultPitch[(int)source];
        useSource.volume = defaultVolume[(int)source];
        useSource.PlayOneShot(clip);
    }
    
    public void PlayClip(AudioSources source, AudioClip clip, float pitchScale, float volumeScale)
    {
        AudioSource useSource = sources[(int)source];
        useSource.pitch = defaultPitch[(int)source] * pitchScale;
        useSource.volume = defaultVolume[(int)source] * volumeScale;
        useSource.PlayOneShot(clip);
    }
    
    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void GetComponents()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        cldr = gameObject.GetComponent<CapsuleCollider>();

        defaultHeight = cldr.height;
        defaultDrag = rb.drag;
        rotFactor = (float)settings.control.lookSensitivity * 0.15f;

        camYaw = playerCam.transform.eulerAngles[1];
        CheckCamInvert();

        this.Inventory = new Inventory();
        Inventory.player = this;
        Inventory.invenFrame = FindObjectOfType<InvenFrame>();
        Inventory.hud = FindObjectOfType<HUD>();

        shadow = transform.GetChild(0).gameObject;

        for (int i = 0; i < playerCam.transform.childCount; i++)
        {
            GameObject child = playerCam.transform.GetChild(i).gameObject;
            if (child.CompareTag("SFX_Move"))
            {
                moveSFX = child.GetComponent<AudioSource>();
            }
            if (child.CompareTag("SFX_Item"))
            {
                itemSFX = child.GetComponent<AudioSource>();
            }
            if (child.CompareTag("SFX_Ambient"))
            {
                ambientSFX = child.GetComponent<AudioSource>();
            }
            if (child.CompareTag("SFX_Environment"))
            {
                environmentSFX = child.GetComponent<AudioSource>();
            }
        }

        sources[0] = moveSFX;
        defaultPitch[0] = moveSFX.pitch;
        defaultVolume[0] = moveSFX.volume;

        sources[1] = itemSFX;
        defaultPitch[1] = itemSFX.pitch;
        defaultVolume[1] = itemSFX.volume;

        sources[2] = ambientSFX;
        defaultPitch[2] = ambientSFX.pitch;
        defaultVolume[2] = ambientSFX.volume;

        sources[3] = environmentSFX;
        defaultPitch[3] = environmentSFX.pitch;
        defaultVolume[3] = environmentSFX.volume;
    }

    private void GetInteractions()
    {
        interacts = ArrayToList(FindObjectsOfType<Interaction>());
    }

    private void CheckCamInvert()
    {
        if (invertPitch)
        {
            pitchDir = 1;
        }
        else if (!invertPitch)
        {
            pitchDir = -1;
        }
    }

    private void Camera()
    {
        float h = cldr.height;
        float r = cldr.radius;
        float x1 = (h - r) / 2;
        playerCam.SetFollow(this.gameObject, x1);
        playerCam.SetRot(new Vector3(camPitch, camYaw, 0.0f));
    }

    public void LockCursor(bool csrLock)
    {
        if (csrLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = true;
            GameManager.isCursorLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
            GameManager.isCursorLocked = false;
        }
    }

    private void HideInteractInds()
    {
        foreach (Interaction target in interacts)
        {
            target.ShowIndicator(false);
        }
    }

    private Interaction GetClosestInteract()
    {
        Interaction closestInteract = null;
        float closestDistance = 9999.9999f;

        foreach (Interaction target in interacts)
        {
            Vector3 disp = target.pos - transform.position;
            float dist = disp.magnitude;
            if (dist < closestDistance && target.isEnabled)
            {
                closestInteract = target;
                closestDistance = dist;
            }
        }

        return closestInteract;
    }

    private IEnumerator HeightTransition(float targetHeight)
    {
        float startHeight = cldr.height;
        float heightDiff = targetHeight - startHeight;

        float aDuration = 0.1f;
        int aFrames = 20;
        float aFrameTime = aDuration / (float)aFrames;

        for (int i = 1; i <= aFrames; i++)
        {
            float delta = (float)i / (float)aFrames;
            yield return new WaitForSeconds(aFrameTime);

            float posY = transform.position[1] + ( heightDiff / (float)aFrames ) / 2.0f;
            transform.position = new Vector3(transform.position[0], posY, transform.position[2]);
            cldr.height = Mathf.Lerp(startHeight, targetHeight, delta);
        }
    }

}
