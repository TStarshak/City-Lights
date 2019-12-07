using System.Collections;
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
    [SerializeField] private GameObject returnMessage;
    [SerializeField] private GameObject upgradeShopMenu;

    private Transform playerTransform;
    private float playerPosX;
    private float playerPosZ;

    private string cityExitPrompt = "enter the forest!";
    private string upgradeShopPrompt = "access the Upgrade Shop!";

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerProgress.Instance.firstTimeInCity()) {
            gameIntroduction.SetActive(true);
        }
        else if (PlayerProgress.Instance.isReturningFromForest()){
            missionResults.SetActive(true);
            PlayerProgress.Instance.returningFromForest = false;
        }
        else {
            returnMessage.SetActive(true);
        }
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Check whether player is eligible to perform an action
        checkforEligibleAction();
    }

    void checkforEligibleAction(){
        if (!PauseController.isPaused){
            // Check for city exit
            if (isNearUpgradeShop()){
                enableButtonPrompt(upgradeShopPrompt);
                enableShopMenuOnAction();
            }
            // Check for upgrade prompt
            else if (isNearCityEdge()){
                enableButtonPrompt(cityExitPrompt);
                exitCityOnAction();
            }
            else{
                // Disable otherwise
               disableButtonPrompt(); 
            }
        }
        else {
            // Disable otherwise
            disableButtonPrompt();
        }
    }

    // Exit the city and load the forest scene
    void exitCityOnAction(){
        if (Input.GetButtonDown("Action")){
            if (!PauseController.isPaused){
                // Reset for next collection phase
                PlayerState.localPlayerData.firefliesCollected = 0;
                SceneController.LoadScene(forestScene);
            }
        }
    }

    // Enable the Upgrade Shop menu
    void enableShopMenuOnAction(){
        if (Input.GetButtonDown("Action")){
            if (!PauseController.isPaused){
                PauseController.disablePauseFunctionality();
                upgradeShopMenu.SetActive(true);
            }
        }
    }

    // Displays the button prompt with the action message
    void enableButtonPrompt(string promptMessage){
        buttonPrompt.text = $"Press <color=#FBE92B><i><b>E</b></i></color> to {promptMessage}";
        buttonPrompt.gameObject.SetActive(true);
    }

    void disableButtonPrompt(){
        buttonPrompt.text = "";
        buttonPrompt.gameObject.SetActive(false);
    }
    
    // Determines if the given position is near the upgrade shop
    bool isNearUpgradeShop(){
        playerPosX = playerTransform.position.x;
        playerPosZ = playerTransform.position.z;
        return (playerPosZ > 2.0f) && (playerPosX > -6.0f && playerPosX < 3.5f);
    }

    // Determines if the given position is near edge of city
    bool isNearCityEdge(){
        playerPosX = playerTransform.position.x;
        return playerPosX > 24.0f || playerPosX < -24.0f;
    }
}
