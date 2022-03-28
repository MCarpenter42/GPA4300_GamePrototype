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

    private bool isOnFloor = false;
    [SerializeField] float jumpStrength = 2.0f;

    // Interaction

    List<Interaction> interacts;

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
        Debug.Log(interacts[0].pos);
    }

    void Update()
    {
        Look();
        Movement();
        Interact();
    }

    void FixedUpdate()
    {
        playerCam.SetRot(new Vector3(camPitch, camYaw));
        CapSpeed();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            isOnFloor = true;
        }
    }

    /* - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - */

    private void Look()
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

    private void Movement()
    {
        float fwdFactor = 0;
        float rhtFactor = 0;
        if (Input.GetKey(controls.movement.forward))
        {
            fwdFactor += maxSpeed;
        }
        if (Input.GetKey(controls.movement.back))
        {
            fwdFactor -= maxSpeed;
        }
        if (Input.GetKey(controls.movement.right))
        {
            rhtFactor += maxSpeed;
        }
        if (Input.GetKey(controls.movement.left))
        {
            rhtFactor -= maxSpeed;
        }
        Vector3 moveForce = transform.forward * fwdFactor + transform.right * rhtFactor;
        rb.AddForce(moveForce);

        if (isOnFloor && Input.GetKeyDown(controls.movement.jump))
        {
            Jump();
        }
    }

    private void Interact()
    {

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

    private void CapSpeed()
    {
        Vector3 flatVel = new Vector3(rb.velocity[0], 0.0f, rb.velocity[2]);
        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 newVel = flatVel.normalized * maxSpeed + new Vector3(0.0f, rb.velocity[1], 0.0f);
            rb.velocity = newVel;
        }
    }

    private void Jump()
    {
        rb.AddForce(new Vector3(0.0f, jumpStrength, 0.0f), ForceMode.Impulse);
        isOnFloor = false;
    }


}
