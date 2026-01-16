using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Healing : MonoBehaviour
{
    /*
    Pseudocode / Plan:
    - If the user assigns a specific GameObject in the inspector, destroy that on pickup.
    - If nothing is assigned (common when editing a prefab asset), fallback to destroying the instantiated prefab's root in the scene.
      -> Use transform.root.gameObject as the fallback because the prefab instance in the scene will be a root or child of a root object.
    - On player trigger: find PlayerHealth on the collider or its parents, heal, then destroy the chosen target.
    */

    // Optional: assign a specific GameObject to destroy. If left null (typical for prefab assets), the script will
    // destroy the instantiated root object in the scene (transform.root.gameObject).
    public GameObject targetToDestroy;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Trigger must be set on this collider (Is Trigger = true) and the Player must have a Collider and Rigidbody as needed.
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        // Try to get PlayerHealth from the collider's GameObject first,
        // then fall back to parent in case the health component is on the root.
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null)
            playerHealth = other.GetComponentInParent<PlayerHealth>();
        print(playerHealth);
        if (playerHealth != null)
        {
            playerHealth.PlayerHeal(40);
            print("Healed 20 ");

            // Destroy the assigned target if set, otherwise destroy this pickup.
            Destroy(targetToDestroy != null ? targetToDestroy : gameObject);
        }


    }
}
