using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CoreFunctionality
{
    #region [ PARAMETERS ]

    // CAMERA & LOOKING

    [SerializeField] PlayerCam playerCam;
    private bool cursorLocked = false;

    private float rotFactor;
    private float camPitch;
    private float camYaw;

    public bool invertPitch;
    private int pitchDir;

    // MOVEMENT

    Rigidbody rb;

    [SerializeField] float maxSpeed = 3.0f;
    [SerializeField] float sprintFactor = 2.0f;
    private Vector3 moveFactors = new Vector3();

    private bool isOnFloor = false;
    [SerializeField] float jumpStrength = 2.0f;

    // Interaction

    List<Interaction> interacts;
    [SerializeField] float interactRange = 3.0f;
    [SerializeField] float interactAngle = 10.0f;
    private bool canInteract = false;

    private Interaction intrClosest;
    private Interaction intrClosestPrev;

    #endregion

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        SetCamera();
        rotFactor = (float)settings.control.lookSensitivity * 0.20f;
        CheckCamInvert();
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
        MoveControl();
        InteractCheck();
    }

    void FixedUpdate()
    {
        playerCam.SetRot(new Vector3(camPitch, camYaw));
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
        camPitch += rotFactor * Input.GetAxis("Mouse Y") * (float)pitchDir;
        camYaw += rotFactor * Input.GetAxis("Mouse X");

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
    }

    private void MoveControl()
    {
        float movMulti = 6.0f;
        if (Input.GetKey(controls.movement.sprint))
        {
            movMulti *= sprintFactor;
        }

        float fwdFactor = 0;
        float rhtFactor = 0;
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

        if (isOnFloor && Input.GetKeyDown(controls.movement.jump))
        {
            moveFactors[1] = jumpStrength;
        }
        else if (!isOnFloor)
        {
            moveFactors[1] = 0.0f;
        }
    }

    private void InteractCheck()
    {
        intrClosest = GetClosestInteract();
        if (intrClosest != intrClosestPrev && intrClosestPrev != null)
        {
            intrClosestPrev.ShowIndicator(false);
        }

        Vector3 disp = intrClosest.pos - transform.position;
        float dist = disp.magnitude;

        Vector3 camDisp = intrClosest.pos - playerCam.transform.position;
        float lookDeviation = Vector3.Angle(playerCam.transform.forward, camDisp);

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

        intrClosestPrev = intrClosest;
    }

    private void MovementPhysics()
    {
        Vector3 velocityFlat = rb.velocity;
        velocityFlat[1] = 0.0f;
        Vector3 relVel = transform.InverseTransformDirection(velocityFlat);
        Vector3 decelForce = new Vector3(0.0f, 0.0f, 0.0f);
        float decelFactor = 8.0f;

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

        Vector3 lateralForce = transform.forward * moveFactors[2] + transform.right * moveFactors[0];
        lateralForce += decelForce;
        rb.AddForce(lateralForce);

        if (relVel.magnitude < 0.05f)
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
        if (Input.GetKey(controls.movement.sprint))
        {
            movMulti *= sprintFactor;
        }

        Vector3 flatVel = new Vector3(rb.velocity[0], 0.0f, rb.velocity[2]);
        if (flatVel.magnitude > maxSpeed * movMulti)
        {
            Vector3 newVel = flatVel.normalized * maxSpeed * movMulti + new Vector3(0.0f, rb.velocity[1], 0.0f);
            rb.velocity = newVel;
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

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

    public void LockCursor(bool csrLock)
    {
        if (csrLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorLocked = false;
        }
    }

    private void SetCamera()
    {
        float h = this.GetComponent<CapsuleCollider>().height;
        float r = this.GetComponent<CapsuleCollider>().radius;
        float x = (h / 2) - r;
        playerCam.SetFollow(this.gameObject, x);
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
        Interaction closestInteract = new Interaction();
        float closestDistance = 9999.9999f;

        foreach (Interaction target in interacts)
        {
            Vector3 disp = target.pos - transform.position;
            float dist = disp.magnitude;
            if (dist < closestDistance)
            {
                closestInteract = target;
                closestDistance = dist;
            }
        }

        return closestInteract;
    }


}
