using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string volumeGroup;

    void Start(){
        slider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);
    }

    // Sets the level of the associated mixer group volume to the value of the slider
    public void setMusicVolume(){
        float sliderValue = slider.value;
        // Sets music vol to slider value as a log of base 10 for the slider value
        mixer.SetFloat(volumeGroup, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }
}
