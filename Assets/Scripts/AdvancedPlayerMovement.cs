using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AdvancedPlayerMovement : MonoBehaviour
{
    // Miscellaneous
    PlayerMovement playerMovement;
    public bool isSliding = false;
    public LayerMask groundLayer;



    // Wall grab and launch variables

        // Modifiable parameters
        public float grabDuration;

        // Wall in range check
        public GameObject wallGrabCheck;
        public Vector3 wallGrabSize;
        
        // To see if you can grab a wall
        public bool isHoldingWall;
        Coroutine wallGrabCoroutine;
        public float minSpeedToWallGrab;
        bool wallGrabIsReady = true;

        // Launch power
        float totalSpeedStore;



    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {


        // Sliding stuff
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isSliding && !isHoldingWall)
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

        // Dashing
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (playerMovement.GetTotalSpeed() <= 50)
            {
                playerMovement.velocity = Camera.main.transform.forward * 25;
            }
            else
            {
                playerMovement.velocity = Camera.main.transform.forward * (playerMovement.GetTotalSpeed() / 2);
            }
        }


        // Wall hold and launch
        if (Input.GetKey(KeyCode.Space) && wallGrabCoroutine == null && playerMovement.GetTotalSpeed() >= minSpeedToWallGrab && Physics.CheckBox(wallGrabCheck.transform.position, wallGrabSize/2, Quaternion.identity, groundLayer) && wallGrabIsReady)
        {
            isHoldingWall = true;
            wallGrabCoroutine = StartCoroutine(WallGrab());
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && isHoldingWall) // Wall release
        {
            playerMovement.freezePlayer = false;
            isHoldingWall = false;
            StopCoroutine(wallGrabCoroutine);
            wallGrabCoroutine = null;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isHoldingWall) // Launches player out of wall grab
        {
            // Undo wall grab
            UndoWallGrab(1.5f);

            // Launch
            playerMovement.velocity = Camera.main.transform.forward * totalSpeedStore;
            totalSpeedStore = 0;

        }

    }



    IEnumerator WallGrab()
    {
        // Set up wall grab, preventing duplicate coroutines and such
        wallGrabIsReady = false;
        playerMovement.freezePlayer = true;
        
        
        totalSpeedStore = playerMovement.GetTotalSpeed();


        yield return new WaitForSeconds(grabDuration);

        // Undoes wall grab, letting it be performed again after a delay
        UndoWallGrab(2);
        totalSpeedStore = 0;
    }

    void WallGrabRecharge()
    {
        wallGrabIsReady = true;
        // Maybe add a visual indicator for it being recharged
    }

    /// <summary>
    /// Sets all variables to what they need to be at to be able to wall grab
    /// </summary>
    /// <param name="rechargeTime">Wall grab cooldown</param>
    void UndoWallGrab(float rechargeTime)
    {
        playerMovement.freezePlayer = false;
        isHoldingWall = false;
        Invoke(nameof(WallGrabRecharge), rechargeTime);
        wallGrabCoroutine = null;
    }
}
