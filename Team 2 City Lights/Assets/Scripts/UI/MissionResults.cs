using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionResults : MonoBehaviour
{
    [SerializeField] private GameObject fireflyterChief;
    [SerializeField] private Text missionResults;
    [SerializeField] private Text firefliesNeeded;
    [SerializeField] private Text collectedFireflies;
    [SerializeField] private Text depositedFireflies;
    [SerializeField] private Text chiefDialogue;
    private MissionHandler.Mission currentMission;
    private PlayerData playerData;
    private Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        fireflyterChief.SetActive(false);
        PauseController.disablePauseFunctionality();
        continueButton = GetComponentInChildren<Button>();
        continueButton.onClick.AddListener(ContinueGame);
        currentMission = MissionHandler.Instance.currentMission;
        playerData = PlayerState.localPlayerData;
        generateMissionResults();
    }

    // Continue the game and into the City Scene
    void ContinueGame(){
        PauseController.enablePauseFunctionality();
        fireflyterChief.SetActive(true);
        gameObject.SetActive(false);
    }

    void generateMissionResults(){
        firefliesNeeded.text = $"{currentMission.fireflyGoal}";
        collectedFireflies.text = $"{playerData.firefliesCollected}";
        if (currentMission.hasMetGoal){
            displayPositiveResults();
        }
        else{
            displayNegativeResults();
        }
    }

    void displayPositiveResults(){
        missionResults.text = "Mission Accomplished";
        depositedFireflies.text = $"{playerData.firefliesCollected - currentMission.fireflyGoal}";
        MissionHandler.Instance.assignNextMission();
        currentMission = MissionHandler.Instance.currentMission;
        chiefDialogue.text = $"Nice work, kid! Next, I'll need you to collect <color=#FBE92B><size=20><i>{currentMission.fireflyGoal}</i></size></color> fireflies!";
    }

    void displayNegativeResults(){
        missionResults.text = "Mission Failed";
        MissionHandler.Instance.resetMission();
        chiefDialogue.text = $"Don't worry, kid. Give it another shot! We need <color=#FBE92B><size=20><i>{currentMission.fireflyGoal}</i></size></color> fireflies!";
        depositedFireflies.text = $"{playerData.firefliesCollected}";
    }
}
