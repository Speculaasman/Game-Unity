using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class C2PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float jumpForce = 6f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    [SerializeField] float smoothTime = 0.05f;
    float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, verticalInput * movementSpeed);


        if (Input.GetButtonDown("Jump") && C2IsGrounded())
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }

        if (horizontalInput == 0 && verticalInput == 0) return;

        var targetAngle = Mathf.Atan2(horizontalInput, verticalInput) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, targetAngle, 0.0f);




    }


    bool C2IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
    }

}
