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
        Button1.SetActive(false);
        Button2.SetActive(false);

    }

    public void Controlls()
    {
        Button1.SetActive(false);
        Button2.SetActive(false);
        Button3.SetActive(false);
        Title.SetActive(false);
        ControlText.SetActive(true);
        ControlText2.SetActive(true);
        Button4.SetActive(true);
        Button5.SetActive(true);
        Button6.SetActive(true);

    }

    public void Back()
    {
        Button1.SetActive(true);
        Button2.SetActive(true);
        Button3.SetActive(true);
        Title.SetActive(true);
        ControlText.SetActive(false);
        ControlText2.SetActive(false)                               ;
        Button4.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
