﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controls the pause menu and pause state of the game
public class PauseController : MonoBehaviour
{
    public static bool isPaused;
    public static bool canPause;

    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject quitButton;

    // Start is called before the first frame update
    void Awake()
    {
        isPaused = false;
        canPause = true;
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetButtonDown("Pause") && pauseIsEnabled())
        {
            togglePauseMenu();
        }

        /* When clicking the UI resume button, the game stays paused despite the menu closing.
            This is a quick fix for that
        */
        if (pauseMenu.activeInHierarchy == false && optionsMenu.activeInHierarchy == false && pauseIsEnabled()){
            resumeGame();
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
                resumeGame();
            }
            else
            {
                pauseMenu.SetActive(true);
                pauseGame();
            }
        }
    }

    public void resumeGame(){
        isPaused = false;
        Time.timeScale = 1;
    }

    public void pauseGame(){
        isPaused = true;
        Time.timeScale = 0;
    }

    public static bool pauseIsEnabled(){
        return canPause;
    }
    
    public static void disablePauseFunctionality(){
        canPause = false;
        isPaused = true;
    }

    public static void enablePauseFunctionality(){
        canPause = true;
        isPaused = false;
    }
}
