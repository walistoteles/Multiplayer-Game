using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform playerCamera = null;
    [SerializeField] float walkSpeed = 6.0f;
    [SerializeField] float mousesensivity = 12.0f;
    [SerializeField] float gravity = -13.0f;
    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;

    [SerializeField] bool lockCursor = true;

    float cameraPitch = 0.0f;
    float velocityY = 0.0f;
    CharacterController controller = null;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    Animator anim;

    public float jumpspeed;
    private float originalSlepOffset;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        originalSlepOffset = controller.stepOffset;
    }

    void Update()
    {
        UpdateMouseLook();
        UpdateMovement();

        float y = controller.velocity.magnitude * Input.GetAxis("Vertical") / 3;
        float x = controller.velocity.magnitude * Input.GetAxis("Horizontal") / 3;

        anim.SetFloat("y", y);
        anim.SetFloat("x", x);
    }

    void UpdateMouseLook()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraPitch -= currentMouseDelta.y * mousesensivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90.0f, 90.0f);

        playerCamera.localEulerAngles = Vector3.right * cameraPitch;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mousesensivity);
    }

    void UpdateMovement()
    {
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        if (controller.isGrounded)
            velocityY = 0.0f;

        velocityY += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded)
        {
            controller.stepOffset = originalSlepOffset;

            velocityY = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                anim.SetTrigger("jump");
                velocityY = jumpspeed;

            }
        }
        else
        {
            controller.stepOffset = 0;
        }
        


        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * walkSpeed + Vector3.up * velocityY;
        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);

    }
}