using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject Title;
    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject ControlText;
    public GameObject ControlText2;
    public GameObject Button4;
    public GameObject Button5;
    public GameObject Button6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Begin()
    {
        SceneManager.LoadScene(2);
        Button1.active = false;
        Button2.active = false;

    }

    public void Controlls()
    {
        Button1.SetActive(false);
        Button2.active = false;
        Button3.active = false;
        Title.active = false;
        ControlText.active = true;
        ControlText2.active = true;
        Button4.active = true;
        Button5.active = true;
        Button6.active = true;

    }

    public void Back()
    {
        Button1.active = true;
        Button2.active = true;
        Button3.active = true;
        Title.active = true;
        ControlText.active = false;
        ControlText2.active = false;
        Button4.active = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
