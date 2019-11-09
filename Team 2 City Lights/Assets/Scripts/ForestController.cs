using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForestController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text buttonPrompt;
    private Transform playerTransform;
    private float playerPosX;
    private float playerPosZ;
    private string cityScene = "City";

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (canEnterCity()){
            enableCityExitPrompt();
        }
        else {
            disableCityExitPrompt();
        }

        if (Input.GetButtonDown("Action") && canEnterCity()){
            SceneController.LoadScene(cityScene);
        }
        
    }

    // Determines if the given position is near edge of city
    bool isNearCityEdge(){
        playerPosX = playerTransform.position.x;
        playerPosZ = playerTransform.position.z;
        return (playerPosX > -9.0f && playerPosX < 6.0f) && (playerPosZ < 4.0f && playerPosZ > -10.0f);
    }
    
    // Toggles the display and functionality of city exit
    void enableCityExitPrompt(){
        //Display Prompt and eligible to exit city
            buttonPrompt.gameObject.SetActive(true);
            buttonPrompt.text = "Press 'E' to enter the city!";
    }

    void disableCityExitPrompt(){
        buttonPrompt.text = "";
        buttonPrompt.gameObject.SetActive(false);
    }

    private bool canEnterCity(){
        return isNearCityEdge() && !PauseController.isPaused && ShadowTimerController.shadowHour;
    }
}

