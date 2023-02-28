using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpSpeed;
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] LayerMask groundMask;
    Animator myAnimator;


    public CameraController cameraController;
    private CharacterController characterController;
    private bool isGrounded;
    RaycastHit hit;

    private Vector3 moveDirection;
    private float verticalVelocity = 0f;
    float horizontal;
    float vertical;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        myAnimator= GetComponent<Animator>();
    }

    private void Update()
    {
        CharacterMovement();
        CharacterJump();
        Firing();
      


    }

    private void Firing()
    {
       if(Input.GetMouseButtonDown(0)) 
        {
            myAnimator.SetBool("isFiring", true);
        }
       else if(Input.GetMouseButtonUp(0)) 
        {
            myAnimator.SetBool("isFiring", false);
        } 
    }

    private void CharacterMovement()
    {
        
            horizontal = Input.GetAxis("Horizontal");
             vertical = Input.GetAxis("Vertical");
            if (horizontal == 0 && vertical == 0)
            {
                myAnimator.SetBool("isWalking", false);
            }
            else
            {
                myAnimator.SetBool("isWalking", true);
            }
        

        
             
           

        

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
