using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JSAM;
using EZCameraShake;
using UnityEngine.Rendering;

public class AdvancedPlayerMovement : MonoBehaviour
{
    // Miscellaneous
    PlayerMovement playerMovement;
    public bool isSliding = false;
    public LayerMask groundLayer;
    public Camera cam;
    public Animator weaponAnim;
    public GameObject metronomeObj;
    Metronome metronome;
    CapsuleCollider collider;
    public ParticleSystem blood;


    // Dash variables
    public GameObject dashVolume;
    public float postInTime;
    public float postOutTime;


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

    public AudioSource LaunchSound;
    public GameObject CartoonParticle1;
    public GameObject CartoonParticle2;
    public GameObject CartoonParticle3;
    public GameObject CartoonParticle4;
    public GameObject CartoonParticle5;




    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        metronome = metronomeObj.GetComponent<Metronome>();
        collider = gameObject.GetComponent<CapsuleCollider>();

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
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashIsReady && !(playerMovement.isGrounded && !isSliding))
        {
            if (playerMovement.GetTotalSpeed() <= 60)
            {
                playerMovement.velocity = Camera.main.transform.forward * 30;
            }
            else
            {
                playerMovement.velocity = Camera.main.transform.forward * (playerMovement.GetTotalSpeed() / 1.5f);
            }

            StartCoroutine(DashPostProcessing());
            dashIsReady = false;
            if (metronome.IsOnBeat())
            {
                Invoke(nameof(DashRecharge), dashCooldown / 4);
            }
            else
            {
                Invoke(nameof(DashRecharge), dashCooldown);
            }
        }


        // Wall hold and launch
        if (Input.GetKey(KeyCode.Space) && wallGrabCoroutine == null && playerMovement.GetTotalSpeed() >= minSpeedToWallGrab && Physics.CheckBox(wallGrabCheck.transform.position, wallGrabSize / 2, Quaternion.identity, groundLayer) && wallGrabIsReady)
        {
            isHoldingWall = true;
            wallGrabCoroutine = StartCoroutine(WallGrab());
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && isHoldingWall) // Wall release
        {
            UndoWallGrab(0.2f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && isHoldingWall) // Launches player out of wall grab
        {
            // Undo wall grab
            UndoWallGrab(0.2f);

            // Launch
            playerMovement.velocity = (Camera.main.transform.forward + new Vector3(0, 0.35f, 0)).normalized * (totalSpeedStore + powerBonus);

            if (totalSpeedStore < 30)
            {
                CameraShaker.Instance.ShakeOnce(20f, 20f, 0.1f, 0.1f);
            }
            if (totalSpeedStore >= 30)
            {
                CameraShaker.Instance.ShakeOnce(20f, 20f, 0.1f, 0.5f);
                LaunchSound.Play();
                // Random number generator 
                int randomnumber = Random.Range(1, 6);

                if (randomnumber == 1)
                {
                    Instantiate(CartoonParticle1, GameObject.Find("WallGrabCheckcapsule").transform.position, Quaternion.identity);
                }
                else if (randomnumber == 2)
                {
                    Instantiate(CartoonParticle2, GameObject.Find("WallGrabCheckcapsule").transform.position, Quaternion.identity);
                }
                else if (randomnumber == 3)
                {
                    Instantiate(CartoonParticle3, GameObject.Find("WallGrabCheckcapsule").transform.position, Quaternion.identity);
                }
                else if (randomnumber == 4)
                {
                    Instantiate(CartoonParticle4, GameObject.Find("WallGrabCheckcapsule").transform.position, Quaternion.identity);
                }
                else if (randomnumber == 5)
                {
                    Instantiate(CartoonParticle5, GameObject.Find("WallGrabCheckcapsule").transform.position, Quaternion.identity);
                }
            }
            totalSpeedStore = 0;







        }

        //Attack Activation
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

    }

        IEnumerator WallGrab()
        {
            // Set up wall grab, preventing duplicate coroutines and such
            wallGrabIsReady = false;
            playerMovement.freezePlayer = true;

            CameraShaker.Instance.ShakeOnce(20f, 20f, 0.05f, 0.05f);

            totalSpeedStore = playerMovement.GetTotalSpeed();


            yield return new WaitForSeconds(grabDuration);

            // Undoes wall grab, letting it be performed again after a delay
            UndoWallGrab(0.2f);
            totalSpeedStore = 0;
        }

        IEnumerator DashPostProcessing()
        {
            float intensity = 0;

            Volume volume = dashVolume.GetComponent<Volume>();

            while (intensity < 1)
            {
                intensity += Time.deltaTime / postInTime;
                Debug.Log(intensity);
                volume.weight = intensity;
                yield return new WaitForEndOfFrame();
                if (intensity >= 1)
                {
                    break;
                }
            }

            while (intensity > 0)
            {
                intensity -= Time.deltaTime / postOutTime;
                Debug.Log(intensity);
                volume.weight = intensity;
                yield return new WaitForEndOfFrame();
                if (intensity <= 0)
                {
                    break;
                }
            }

            StopCoroutine(DashPostProcessing());
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
        StartCoroutine(nameof(AttackRaycast), attackDelay);

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

    IEnumerator AttackRaycast()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
            print("raycast fired and hit");
            if (hit.transform.TryGetComponent<Actor>(out Actor T))
            { 
                if (metronome.IsOnBeat())
                {
                    AudioManager.PlaySound(AudioLibrayrSounds.SynthHit);
                    AudioManager.PlaySound(AudioLibrayrSounds.SynthHit);
                    Time.timeScale = 0.05f;
                    yield return new WaitForSecondsRealtime(0.7f);
                    T.TakeDamage(attackDamage * 2);
                    Time.timeScale = 1f;
                    CameraShaker.Instance.ShakeOnce(20f, 20f, 0.05f, 0.25f);
                }
                else
                {
                    T.TakeDamage(attackDamage);
                    Time.timeScale = 0.1f;
                }
                AudioManager.PlaySound(AudioLibrayrSounds.SynthHit);
                AudioManager.PlaySound(AudioLibrayrSounds.ImpactSOund);
                
                if (metronome.IsOnBeat())
                {
                    yield return new WaitForSecondsRealtime(0.3f);
                }
                else
                {
                    yield return new WaitForSecondsRealtime(0.15f);
                }
                    Time.timeScale = 1;
            }
        }
    }

    void HitTarget(Vector3 pos)
    {

    }

    void WallGrabRecharge()
    {
        wallGrabIsReady = true;
        // Maybe add a visual indicator for it being recharged
    }



    void DashRecharge()
    {
        dashIsReady = true;
    }
}


