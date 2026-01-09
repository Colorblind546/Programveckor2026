using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{


    // Player Movement 
    [Header("Movement")]

     Rigidbody rb;

    public Transform orientation; 

    public float MoveSpeed;

    public float maxSpeed;

    float HorizontalInput;

    float verticalInput;

    Vector3 moveDirection;

    public AudioSource walking; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;




        // Set the raycast to be slighly beneath the players feet

        // no 

        // Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false; 



    }

    

    // Update is called once per frame
    void Update()
    {



       

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            walking.enabled = true;
        }
        else
        {
            
                walking.enabled = false;
            
        }
        

        MyInput();
    }

    private void MyInput()
    {
        HorizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        MovePlayer();

      
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * HorizontalInput;
        rb.AddForce(moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);
        //rb.AddForce(moveDirection.normalized * MoveSpeed * 10f, ForceMode.Force);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }

        //Apply movement to rigidbody


        // If we aren't moving and are on the ground, stop velocity so we don't slide

    }


}