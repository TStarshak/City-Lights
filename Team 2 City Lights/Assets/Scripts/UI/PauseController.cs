using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the pause menu and pause state of the game
public class PauseController : MonoBehaviour
{
    public static bool isPaused = false;

    public GameObject pauseMenu;
    public GameObject optionsMenu;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetButtonDown("Pause"))
        {
            togglePauseMenu();
        }
    }

     // Toggles the Pause State of the game
    void togglePauseMenu(){
        if (optionsMenu.activeInHierarchy)
        {
            optionsMenu.SetActive(false);
        }
        else
        {
            if (pauseMenu.activeInHierarchy)
            {
                pauseMenu.SetActive(false);
                isPaused = false;
            }
            else
            {
                pauseMenu.SetActive(true);
                isPaused = true;
            }
        }
    }
}
