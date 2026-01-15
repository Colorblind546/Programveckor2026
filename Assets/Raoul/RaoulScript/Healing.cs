using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public GameObject gameObject1;
    Rigidbody rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Heal the player by 20 health points
                playerHealth.PlayerHeal(20);
                // Destroy the healing item after use
                Destroy(); 
            }
        }
    }

    private void Destroy()
    {
        Destroy(gameObject1);
    }
}
