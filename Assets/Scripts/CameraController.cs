using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] float sensitivity = 5.0f;
   
    [SerializeField] Vector3 offset = new Vector3(0, 1, -3);
    [SerializeField] Transform playerTransform;
  

    private Vector2 currentRotation;

    private void Awake()
    {
        
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        // Set the camera's rotation to face forward for a few seconds
        
    }

 

        void Update()
       {
         float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        

        currentRotation.x += mouseX;
         currentRotation.y = 0;
  

        transform.eulerAngles = new Vector3(currentRotation.y, currentRotation.x, 0);
        transform.position = playerTransform.position + offset;
       }
    //[SerializeField] float sensitivity = 5.0f;
    //[SerializeField] float maxYAngle = 90.0f;
    //[SerializeField] Vector3 offset = new Vector3(0, 1, -3);
    //[SerializeField] Transform playerTransform;

    //private Vector2 currentRotation;

    //private void Start()
    //{
    //    Cursor.lockState = CursorLockMode.Locked;
    //}
    //void Update()
    //{
    //    float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
    //    float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

    //    currentRotation.x += mouseX;
    //    currentRotation.y -= mouseY;
    //    currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

    //    transform.eulerAngles = new Vector3(currentRotation.y, currentRotation.x, 0);

    //    transform.position = playerTransform.position + offset;
    //    transform.rotation = playerTransform.rotation;
    //}

}
