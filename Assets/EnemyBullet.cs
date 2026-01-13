using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField]
    float BulletSpeed = 1f;
    Rigidbody rb;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(BulletTimer());

    }
    void Start()
    {
        rb.velocity = transform.forward * BulletSpeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hit " + collision.gameObject.name);
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.PlayerTakeDamage(10);
        }
        Destroy(gameObject);
    }
    IEnumerator BulletTimer()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
