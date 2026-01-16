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


    public void PlayerHeal(int heal)
    {
        playerhealth += heal;
        if (playerhealth > 100)
        {
            playerhealth = 100;
        }
    }

    private void DestroyPlayer()
    {
        SceneManager.LoadScene(5);
    }

}
