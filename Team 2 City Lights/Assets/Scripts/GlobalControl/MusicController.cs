using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{

    public AudioMixer mixer;
    public Slider slider;

    void Start(){
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    // Sets the level of the mixer volume to the value of the slider
    public void setVolume(){
        float sliderValue = slider.value;
        // Sets music vol to slider value as a log of base 10 for the slider value
        mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
