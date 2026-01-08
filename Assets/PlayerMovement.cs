using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    float moveX;
    float moveZ;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");

        Debug.Log($"X:{moveX}  Y:{moveZ}");

        transform.position += new Vector3(moveX * 10, 0, moveZ * 10) * Time.deltaTime;


    }
}
