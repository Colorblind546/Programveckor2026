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


    // Collision checks and the like
    /// <summary>
    /// Whether or not the player is grounded
    /// </summary>
    bool isGripGate = true;
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

        isGripGate = Physics.CheckSphere(groundCheck.transform.position, checkRadius, groundLayer);




        if (isGripGate)
        {
            moveX = Input.GetAxis("Horizontal") * moveSpeed;
            moveZ = Input.GetAxis("Vertical") * moveSpeed;
            velocity = (transform.right * moveX + transform.forward * moveZ) + Vector3.up * -3;
            
        }

        if (!isGripGate)
        {
            accelerateX = Input.GetAxisRaw("Horizontal") * airControl;
            accelerateZ = Input.GetAxisRaw("Vertical") * airControl;

            velocity += (transform.right * accelerateX + transform.forward * accelerateZ) * Time.deltaTime;
            velocity += new Vector3(0, Physics.gravity.y, 0) * Time.deltaTime;

            
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && isGripGate)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y); ;
        }


        controller.Move(velocity * Time.deltaTime);


    }

}
