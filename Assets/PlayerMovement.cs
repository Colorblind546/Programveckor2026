using System.Collections;
using System.Collections.Generic;
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
    Vector3 velocity;


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
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
            controller.Move(new Vector3(moveX * moveSpeed, 0, moveZ * moveSpeed) * Time.deltaTime);
            controller.Move(new Vector3(0, -3, 0) * Time.deltaTime);
        }

        if (!isGripGate)
        {
            accelerateX = Input.GetAxisRaw("Horizontal");
            accelerateZ = Input.GetAxisRaw("Vertical");

            velocity += new Vector3(accelerateX, 0, accelerateZ) * Time.deltaTime;
            velocity += new Vector3(0, Physics.gravity.y, 0) * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }
        



    }

}
