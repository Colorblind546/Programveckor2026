using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Healing : MonoBehaviour
{
    /*
    Pseudocode / Plan (detailed):
    - Exposed fields:
      - public GameObject targetToDestroy: optional inspector reference (may point to a prefab asset or a scene instance)
      - public int healAmount: amount to heal the player
    - On Awake:
      - cache Rigidbody with GetComponent<Rigidbody>();
    - Provide a single helper method `ProcessPickup(Collider otherCollider)`:
      - If the collider's GameObject does not have tag "Player", return.
      - Get PlayerHealth using `GetComponentInParent<PlayerHealth>()` starting from the collider's GameObject.
      - If no PlayerHealth found, return.
      - Call `PlayerHeal(healAmount)` and log the heal amount.
      - Determine which GameObject to destroy:
        - If `targetToDestroy` is not null and `targetToDestroy.scene.IsValid()` is true, destroy `targetToDestroy`.
        - Otherwise, destroy the instantiated scene instance that this script belongs to:
          - Prefer `transform.root.gameObject` (the instantiated root) if available.
          - Fallback to `gameObject`.
      - Call `Destroy(toDestroy)`.
    - Wire `ProcessPickup` from both `OnTriggerEnter(Collider)` and `OnCollisionEnter(Collision)`:
      - This covers both trigger-based and physics-collision pickups.
    - Rationale:
      - Many pickup issues come from using non-trigger colliders or assigning a prefab asset in the inspector.
      - This approach handles both collision types and detects prefab assets to destroy the actual scene instance.
    */

    // Optional: If set to a scene instance, that instance will be destroyed on pickup.
    // If left null or set to a prefab asset, the script will destroy the instantiated root in the scene.
    public GameObject targetToDestroy;

    // Amount to heal the player
    public int healAmount = 40;

    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Called when using trigger colliders (requires this collider to have Is Trigger = true).
    private void OnTriggerEnter(Collider other)
    {
        ProcessPickup(other);
    }

    // Called when colliders are not triggers and physics collisions occur.
    private void OnCollisionEnter(Collision collision)
    {
        // Use the collision's collider to identify the other object that hit us.
        if (collision == null || collision.collider == null)
            return;

        ProcessPickup(collision.collider);
    }

    // Centralized pickup processing for both trigger and collision events.
    private void ProcessPickup(Collider otherCollider)
    {
        if (otherCollider == null)
            return;

        if (!otherCollider.CompareTag("Player"))
            return;

        // Attempt to find PlayerHealth on the collider or any parent (covers common setups).
        PlayerHealth playerHealth = otherCollider.GetComponent<PlayerHealth>();
        if (playerHealth == null)
            playerHealth = otherCollider.GetComponentInParent<PlayerHealth>();

        if (playerHealth == null)
            return;

        playerHealth.PlayerHeal(healAmount);
        Debug.Log($"Healed {healAmount}");

        // Determine which GameObject instance to destroy.
        GameObject toDestroy = targetToDestroy;

        // If targetToDestroy is null or points to a prefab asset (non-scene object),
        // fall back to destroying the instantiated root in the scene.
        if (toDestroy == null || !toDestroy.scene.IsValid())
        {
            // transform.root is always valid; prefer the root of this transform so we remove the whole pickup instance.
            toDestroy = transform.root != null ? transform.root.gameObject : gameObject;
        }

        Destroy(toDestroy);
    }
}
