using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour

{
    public GameObject pauseMenu;
    public bool isPaused;

    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    void Update()
    {
        // Only pause/unpause if tutorial is not active
        if (Tutorial.isTutorial)
            return;

        // Escape toggles pause
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}



