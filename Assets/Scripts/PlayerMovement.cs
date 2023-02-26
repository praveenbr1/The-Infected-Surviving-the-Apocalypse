using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] LayerMask groundMask;

    public CameraController cameraController;
    private CharacterController characterController;
    private bool isGrounded;
    RaycastHit hit;

    private Vector3 moveDirection;
    private float verticalVelocity = 0f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        CharacterMovement();
        CharacterJump();
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

        moveDirection = direction * moveSpeed;

        // Apply gravity
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        // Combine movement and gravity
        moveDirection.y = verticalVelocity;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void CharacterJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpSpeed * -2f * Physics.gravity.y);
        }
    }

    private void FixedUpdate()
    {
        //You can write below code also instead of RayCast...
        // isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask, QueryTriggerInteraction.Ignore);
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, 0.5f, groundMask);
    }
}
