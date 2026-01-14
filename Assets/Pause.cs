using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject Tutorialmenu;
    public static bool ispuased;

    void Start()
    {
        Tutorialmenu.SetActive(false);
    }
    public void pause()
    {
        Tutorialmenu.SetActive(true);
        Time.timeScale = 0f;
        Debug.Log("Game is paused");
    }
    public void StartTime()
    {
        Tutorialmenu.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Game is no longer paused");
    }
}
