using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Your friendly neighborhood script for controlling audio on the main menu
public class MainMenuAudio : MonoBehaviour
{
    public AudioClip mainMenuTrack;
    public AudioClip creditsTrack;

    private AudioSource audioSource;

    public
    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void toggleSong(){
        audioSource.clip = (audioSource.clip == mainMenuTrack) ? creditsTrack : mainMenuTrack;
        audioSource.Play();
        audioSource.loop = true;
    }
}
