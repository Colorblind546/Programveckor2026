using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changeintro : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)|| (Input.GetMouseButtonDown(0)))
        {
            NextScene();
        }
    }
    public void NextScene()
    {
        Debug.Log("switch to next scene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // Laddar scener eller nivår för spelet
    }

}
