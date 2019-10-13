using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

//Used to control City Management
public class CityController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Text buttonPrompt;
    [SerializeField] private string forestScene;
    private Transform playerTransform;
    private float playerPosX;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isNearCityEdge()){
            enableCityExitPrompt();
        }
        else {
            disableCityExitPrompt();
        }

        if (Input.GetButtonDown("Action")){
            if (isNearCityEdge()){
                SceneManager.LoadScene(forestScene);
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
            buttonPrompt.text = "Press 'E' to exit the city!";
    }
    void disableCityExitPrompt(){
        buttonPrompt.text = "";
        buttonPrompt.gameObject.SetActive(false);
    }
}
