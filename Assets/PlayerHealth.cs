using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int playerhealth = 100;
 
    public void PlayerTakeDamage(int damage)
    {
        playerhealth -= damage;
        if (playerhealth <= 0)
        {
            Invoke(nameof(DestroyPlayer), .5f);
        }

    }

    private void DestroyPlayer()
    {
        SceneManager.LoadScene(1);
    }

}
