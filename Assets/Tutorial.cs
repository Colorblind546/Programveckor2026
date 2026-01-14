using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Tutorialmenu;
    public static bool ispuased;
    public static bool TurnofTutorial;
    
    void Start()
    {
        TurnofTutorial = false;
        Tutorialmenu.SetActive(false);
        ispuased = false;
    }
    public void pause()
    {
        if (TurnofTutorial == false)
        {
            Tutorialmenu.SetActive(true);
            Time.timeScale = 0f;
            Debug.Log("Game is paused");
            ispuased = true;
        }
    }
    public void StartTime()
    {
        if (TurnofTutorial==false)
        {
            Tutorialmenu.SetActive(false);
            Time.timeScale = 1f;
            Debug.Log("Game is no longer paused");
            ispuased = false;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && (ispuased==true))
        {
            TurnofTutorial = true;
            StartTime();
        }
    }
}
