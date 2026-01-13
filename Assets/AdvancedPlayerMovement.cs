using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using JSAM;

public class AdvancedPlayerMovement : MonoBehaviour
{
    // Miscellaneous
    PlayerMovement playerMovement;
    public bool isSliding = false;
    public LayerMask groundLayer;
    public Camera cam;
    public Animator weaponAnim;

    // Dash variables

        // Dash cooldown variables
        public float dashCooldown;
        public bool dashIsReady = true;

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
        public float powerBonus;



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
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashIsReady)
        {
            if (playerMovement.GetTotalSpeed() <= 60)
            {
                playerMovement.velocity = Camera.main.transform.forward * 30;
            }
            else
            {
                playerMovement.velocity = Camera.main.transform.forward * (playerMovement.GetTotalSpeed() / 1.5f);
            }
            dashIsReady = false;
            Invoke(nameof(DashRecharge), dashCooldown);
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
            UndoWallGrab(0.5f);

            // Launch
            playerMovement.velocity = Camera.main.transform.forward * (totalSpeedStore + powerBonus);
            totalSpeedStore = 0;

        }

        //Attack Activation
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
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
    }

    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 1;
    public LayerMask attackLayer;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;


    public void Attack()
    {
        
            if (!readyToAttack || attacking) return;

            readyToAttack = false;
            attacking = true;

            
            AudioManager.PlaySound(AudioLibrayrSounds.WooshSound);

        weaponAnim.SetTrigger("attack");

            print("started attack");

            Invoke(nameof(ResetAttack), attackSpeed);
            Invoke(nameof(AttackRaycast), attackDelay);

            if (attackCount == 0)
            {
                attackCount++;
            }
            else
            {
                attackCount = 0;
            }
        } 
    

    void ResetAttack()
    {
        attacking = false;
        readyToAttack = true;
        weaponAnim.ResetTrigger("attack");
    }

    void AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
            print("raycast fired and hit");
            if (hit.transform.TryGetComponent<Actor>(out Actor T))
            { 
                T.TakeDamage(attackDamage);
                AudioManager.PlaySound(AudioLibrayrSounds.SynthHit);
                AudioManager.PlaySound(AudioLibrayrSounds.ImpactSOund);
            }
        }
    }

    void HitTarget(Vector3 pos)
    {
       
        // Undoes wall grab, letting it be performed again after a delay
        UndoWallGrab(1f);
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
        StopCoroutine(wallGrabCoroutine);
        wallGrabCoroutine = null;
    }

    void DashRecharge()
    {
        dashIsReady = true;
    }
}


