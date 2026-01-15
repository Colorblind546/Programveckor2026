using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialMenu;
    public static bool isTutorial;
    public static bool TurnofTutorial;
    public bool hasBeenShown = false;


    void Start()
    {
        tutorialMenu.SetActive(false);
        isTutorial = false;
    }

    // Call this to show the tutorial (e.g., trigger)
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
        // Only allow Escape to close tutorial if it's active
        if (isTutorial && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTutorial();
        }
    }

    void CloseTutorial()
    {
        tutorialMenu.SetActive(false);
        isTutorial = false;
        Time.timeScale = 1f; // resume game
    }
}
