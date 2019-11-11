﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Used to control City Management and interactions
public class CityController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text buttonPrompt;
    [SerializeField] private string forestScene;
    [SerializeField] private GameObject gameIntroduction;
    [SerializeField] private GameObject missionResults;

    private Transform playerTransform;
    private float playerPosX;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerProgress.Instance.firstTimeInCity()) {
            gameIntroduction.SetActive(true);
        }
        else {
            missionResults.SetActive(true);
        }

        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearCityEdge() && !PauseController.isPaused){
            enableCityExitPrompt();
        }
        else {
            disableCityExitPrompt();
        }

        if (Input.GetButtonDown("Action") && isNearCityEdge()){
            if (!PauseController.isPaused){
                // Reset for next collection phase
                PlayerState.localPlayerData.firefliesCollected = 0;
                SceneController.LoadScene(forestScene);
            }
        }
        
    }

    // Determines if the given position is near edge of city
    bool isNearCityEdge(){
        playerPosX = playerTransform.position.x;
        return playerPosX > 24.0f || playerPosX < -24.0f;
    }
    
    // Toggles the display and functionality of city exit
    void enableCityExitPrompt(){
        //Display Prompt and eligible to exit city
            buttonPrompt.gameObject.SetActive(true);
            buttonPrompt.text = "Press 'E' to enter the forest!";
    }
    void disableCityExitPrompt(){
        buttonPrompt.text = "";
        buttonPrompt.gameObject.SetActive(false);
    }
}
