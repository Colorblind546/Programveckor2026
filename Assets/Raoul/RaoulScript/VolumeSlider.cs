using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider; 
    public AudioSource audioSource; 

    void Start()
    {
        // Set slider value to current volume (if any)
        volumeSlider.value = audioSource.volume;
        // Add listener to the slider's OnValueChanged event
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }
}
