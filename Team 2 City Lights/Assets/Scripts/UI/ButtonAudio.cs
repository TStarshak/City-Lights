using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour
{

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<Button>().onClick.AddListener(playAudioOnClick);
    }

    void playAudioOnClick(){
        audioSource.Play();
    }
}

    
