using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialMenu;    
    public bool hasBeenShown = false; 

    public static bool isTutorial = false;

    void Start()
    {
        tutorialMenu.SetActive(false);
        if (tutorialMenu != null) tutorialMenu.SetActive(false);
    }

    // Call this when player enters the trigger
    public void ShowTutorial()
    {
        if (hasBeenShown) return; // already shown, do nothing

        tutorialMenu.SetActive(true);
        if (tutorialMenu != null) tutorialMenu.SetActive(true);

        isTutorial = true;
        Time.timeScale = 0f;

        hasBeenShown = true; // mark as shown
    }

    void Update()
    {
        // Only allow Escape if tutorial is active
        if (isTutorial && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        tutorialMenu.SetActive(false);
        if (tutorialMenu != null) tutorialMenu.SetActive(false);

        isTutorial = false;
        Time.timeScale = 1f;
    }
}

