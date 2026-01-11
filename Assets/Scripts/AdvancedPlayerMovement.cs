using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvancedPlayerMovement : MonoBehaviour
{

    public bool isSliding = false;

    PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding)
        {
            isSliding = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && isSliding)
        {
            isSliding = false;
        }

        if (isSliding)
        {
            transform.localScale = new Vector3(1, 0.5f, 1);
        }
        if (!isSliding)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
