using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float moveSpeed = 5.0f;
   
    public CameraController cameraController;
    private CharacterController characterController;
    float gravity = -9.81f;

    Vector3 gravitatinalForce;
    [SerializeField] Transform groundCheck;
    float groundDistance = 0.1f;
    [SerializeField] LayerMask groundMask;
    bool touchesGround;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        

    }

    void Update()
    {
        GravityCheck();

        CharacterMovement();

        CharacterJump();
        transform.rotation = Quaternion.Euler(0, cameraController.transform.rotation.eulerAngles.y, 0);
    }

    private void CharacterJump()
    {
        if (Input.GetButtonDown("Jump") && touchesGround)
        {
            gravitatinalForce.y = Mathf.Sqrt(jumpSpeed * -2f * gravity);
        }
    }

    private void GravityCheck()
    {
        touchesGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (touchesGround && gravitatinalForce.y < 0)
        {
            gravitatinalForce.y = -2f;
        }
        gravitatinalForce.y += gravity * Time.deltaTime;
    }

    private void CharacterMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0.0f, vertical);
        direction = cameraController.transform.TransformDirection(direction);
        direction.y = 0.0f;
        direction.Normalize(); // normalize to prevent diagonal movement

        Quaternion cameraHorizontalRotation = Quaternion.Euler(0, cameraController.transform.rotation.eulerAngles.y, 0);
        transform.rotation = cameraHorizontalRotation;

        Vector3 moveDirection = direction * moveSpeed * Time.deltaTime;
        characterController.Move(moveDirection);
        characterController.Move(gravitatinalForce * Time.deltaTime);


    }
}
