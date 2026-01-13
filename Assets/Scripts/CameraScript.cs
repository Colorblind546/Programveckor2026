using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    // Camera Rotation 

    // Variables

    public Transform Player; 
    public float MouseSensativity = 2f;
    private float verticalRotation = 0f;

    bool lockedCursor = true; 

    // Start is called before the first frame update
    void Start()
    {

        // Lock and hide cursor
        UnityEngine.Cursor.visible = false;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked; 


    }



    // Update is called once per frame
    void Update()
    {

        // Collect Input 

         float inputX = Input.GetAxis("Mouse X"); 
         float inputY = Input.GetAxis("Mouse Y"); 


        // Rotate camera around its local X axis 

         verticalRotation -= inputY; 
         verticalRotation = Mathf.Clamp (verticalRotation, -90f, 90f );
         transform.localEulerAngles = Vector3.right*verticalRotation; 


        // Rotate the Player Object around its Y axis

         Player.Rotate(Vector3.up*inputX);







    }

    void FixedUpdate()
    {
        
    }

   


  
}