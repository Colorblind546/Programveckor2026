using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Soundscript : MonoBehaviour
{
    public AudioSource ScarySound;
    public AudioSource SecondScare; 
    public GameObject ThePlayer;
  

    private void OnTriggerEnter(Collider other)
    {
       
            ScarySound.Play();


            StartCoroutine(Thing());
        
    }

    private IEnumerator Thing()
    {
        yield return new WaitForSeconds(1.25f);
        SecondScare.Play();
    }

    IEnumerator Endjump()
    {
        yield return new WaitForSeconds(2.03f);
        ThePlayer.SetActive(true);
       
    }
}
