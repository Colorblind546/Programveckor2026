using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    CharacterController controller;
    // General movement stuff
    public Vector3 velocity;
    AdvancedPlayerMovement advancedPlayerMovement;
    // Grounded movement stuff
    public float moveSpeed;
    


    // Airborne movement
    bool groundedLastUpdate = true;
        // Air control stuff
        public float airControl;
        public float highSpeedAirControl;
        public float lowSpeedAirControl;
        public float airControlSpeedThreshold;


    // Miscellaneous 
    public float jumpHeight;
    private float totalSpeed;
    public bool freezePlayer = false;


    // Collision checks and the like
   
    public bool isGrounded = true;
    public GameObject groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        advancedPlayerMovement = GetComponent<AdvancedPlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        // Checks if the player is grounded, and assigns the value to isGrounded
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, groundLayer) && velocity.y <= 1;
        
        // Air control speed modifier
        if (totalSpeed >  airControlSpeedThreshold)
        {
            airControl = highSpeedAirControl;
        }
        else if (totalSpeed < airControlSpeedThreshold)
        {
            airControl = lowSpeedAirControl;
        }





        // Freezes the player in place
        if (freezePlayer)
        {
            velocity = Vector3.zero;
        }





        // Handles movement when grounded and not sliding
        else if (isGrounded && !advancedPlayerMovement.isSliding)
        {
            // Makes sure that the player doesn't move faster on diagonals
            float axisMagnitude = Mathf.Sqrt(Input.GetAxis("Horizontal") * Input.GetAxis("Horizontal") + Input.GetAxis("Vertical") * Input.GetAxis("Vertical"));
            if (axisMagnitude <= 1)
            {
                axisMagnitude = 1;
            }

            float moveX = Input.GetAxis("Horizontal") / axisMagnitude * moveSpeed;
            float moveZ = Input.GetAxis("Vertical") / axisMagnitude * moveSpeed;

            velocity = (transform.right * moveX + transform.forward * moveZ) + Vector3.up * -7.5f;

        }





        // Handles movement when sliding
        else if (advancedPlayerMovement.isSliding && isGrounded)
        {

            velocity.y = -7.5f; // Keeps the player grounded

        }





        // Handles movement when airborne
        else if (!isGrounded)
        {
            // Makes sure that the player doesn't accelerate faster on diagonals
            float axisMagnitude = Mathf.Sqrt(Input.GetAxisRaw("Horizontal") * Input.GetAxisRaw("Horizontal") + Input.GetAxisRaw("Vertical") * Input.GetAxisRaw("Vertical"));
            if (axisMagnitude <= 1)
            {
                axisMagnitude = 1;
            }

            if (groundedLastUpdate && !isGrounded && velocity.y < 0)
            {
                velocity.y = 0;
            }


            float accelerateX = Input.GetAxisRaw("Horizontal") / axisMagnitude * airControl;
            float accelerateZ = Input.GetAxisRaw("Vertical") / axisMagnitude * airControl;

            velocity += (transform.right * accelerateX + transform.forward * accelerateZ) * Time.deltaTime;
            velocity += new Vector3(0, Physics.gravity.y, 0) * Time.deltaTime;

        }
        
        // Handles jumping, and stops upward velocity respectively
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y); ;
        }

        controller.Move(velocity * Time.deltaTime); // Applies movement and moves the player


        totalSpeed = new Vector3(velocity.x, 0, velocity.z).magnitude; // Gets the total velocity of the player as a float
        groundedLastUpdate = isGrounded; // Sets grounded on last frame to current grounded state
    }

    public float GetTotalSpeed()
    {
        return totalSpeed;
    }

}
