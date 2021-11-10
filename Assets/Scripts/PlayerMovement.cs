using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2;
    public Rigidbody rb;
    public float jumpHeight;

    [SerializeField]
    private Transform cam;

    [SerializeField]
    private float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    [SerializeField]
    private Transform groundCheck;

    [SerializeField]
    private float groundDistance = 0.5f;

    [SerializeField]
    private LayerMask groundMask;

    [SerializeField]
    private bool isGrounded;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;           //locks the cursor
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);         //check if grounded

        if(isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpHeight);           //if grounded then can jump
            }
        }

    }


    void FixedUpdate()
    {
        float movementHorizontal = Input.GetAxis("Horizontal");     
        float movementVertical = Input.GetAxis("Vertical");                                                                         //checks movements
        Vector3 direction = new Vector3(movementHorizontal, 0f, movementVertical).normalized;               
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);      //checks the direction of the camera 
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            rb.AddForce(moveDir * speed);                                                                                           //moves with the camera
        }
    }
}
