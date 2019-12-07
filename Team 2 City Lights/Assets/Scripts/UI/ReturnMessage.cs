using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReturnMessage : MonoBehaviour
{
    [SerializeField] private Text returntext;
    [SerializeField] private GameObject fireflyterChief;

    private MissionHandler.Mission currentMission;
    private PlayerData playerData;
    private Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        // Enable menu functionality
        fireflyterChief.SetActive(false);
        PauseController.disablePauseFunctionality();
        continueButton = GetComponentInChildren<Button>();
        continueButton.onClick.AddListener(ContinueGame);

        // Grab saved mission data
        currentMission = MissionHandler.Instance.currentMission;
        playerData = PlayerState.localPlayerData;
        returntext.text += $"As a reminder, we need you to collect <color=#FBE92B><size=20><i>{currentMission.fireflyGoal}</i></size></color> fireflies! Get out there, kid!";
    }

    // Continue the game and into the City Scene
    void ContinueGame(){
        PauseController.enablePauseFunctionality();
        fireflyterChief.SetActive(true);
        gameObject.SetActive(false);
    }
}
