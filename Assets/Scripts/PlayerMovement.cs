using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;

    // Grounded movement stuff
    float moveX;
    float moveZ;
    public float moveSpeed;
    

    // Airborne and sliding movement
    float accelerateX;
    float accelerateZ;
    public float airControl;
    Vector3 velocity;

    // Miscellaneous 
    public float jumpHeight;
    private float totalSpeed;


    // Collision checks and the like
    /// <summary>
    /// Whether or not the player is grounded
    /// </summary>
    bool isGrounded = true;
    public GameObject groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player is grounded, and assigns the value to isGrounded
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, groundLayer);

        

        // Handles movement when grounded and not sliding
        if (isGrounded)
        {
            // Makes sure that the player doesn't move faster on diagonals
            float axisMagnitude = Mathf.Sqrt(Input.GetAxis("Horizontal") * Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") * Input.GetAxis("Vertical"));
            if (axisMagnitude <= 1)
            {
                axisMagnitude = 1;
            }


            moveX = Input.GetAxis("Horizontal") / axisMagnitude * moveSpeed;
            moveZ = Input.GetAxis("Vertical") / axisMagnitude * moveSpeed;

            velocity = (transform.right * moveX + transform.forward * moveZ) + Vector3.up * -3;
            
        }


        // Handles movement when airborne or sliding
        if (!isGrounded)
        {
            // Makes sure that the player doesn't accelerate faster on diagonals
            float axisMagnitude = Mathf.Sqrt(Input.GetAxisRaw("Horizontal") * Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical") * Input.GetAxisRaw("Vertical"));
            if (axisMagnitude <= 1)
            {
                axisMagnitude = 1;
            }


            accelerateX = Input.GetAxisRaw("Horizontal") / axisMagnitude * airControl;
            accelerateZ = Input.GetAxisRaw("Vertical") / axisMagnitude * airControl;

            velocity += (transform.right * accelerateX + transform.forward * accelerateZ) * Time.deltaTime;
            velocity += new Vector3(0, Physics.gravity.y, 0) * Time.deltaTime;

        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y); ;
        }



        totalSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude;

        controller.Move(velocity * Time.deltaTime);


    }

    public float GetTotalSpeed()
    {
        return totalSpeed;
    }

}
