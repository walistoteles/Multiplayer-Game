using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{



    [Header("Movement")]
    public float moveSpeed = 6f;
    [SerializeField] Transform orientation;

    public KeyCode jumpKey = KeyCode.Space;

    [Header("Jump")]
    public float jumpForce = 5f;

    float playerHight = 2f;
    float movementmultiplier = 10f;
    [SerializeField] float airmultiplier = 0.4f;
    float groundDrag = 6f;
    float airDrag = 4f;
    float horizontalMovementSpeed;
    float verticalMovementSpeed;

    bool isGrounded;

    Vector3 moveDirection;



    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }


    void MyInput()
    {
        horizontalMovementSpeed = Input.GetAxisRaw("Horizontal");
        verticalMovementSpeed = Input.GetAxisRaw("Vertical");

        moveDirection = orientation.forward * verticalMovementSpeed + orientation.right * horizontalMovementSpeed; ;

    }


    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;

        }
        else
        {
            rb.drag = airDrag;
        }
    }

    private void Update()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHight / 2 + 0.1f);

        MyInput();
        ControlDrag();
        //anim.SetFloat("move", rb.velocity.magnitude * 0.1f);

        if (Input.GetKeyDown(jumpKey) && isGrounded)
        {
            // Jump
            Jump();
        }
    }

    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    void MovePlayer()
    {
        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementmultiplier, ForceMode.Acceleration);

        }
        else
        {

            rb.AddForce(moveDirection.normalized * moveSpeed * movementmultiplier * airmultiplier, ForceMode.Acceleration);
        }
    }
}
