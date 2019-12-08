using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionHandler : MonoBehaviour
{

    public static MissionHandler Instance;
    public PlayerData.Mission currentMission;

    // Start is called before the first frame update
    void Awake()
    {
         // Instantiate this tracker if none exists yet and keep game object alive
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        // Otherwise prevent duplicates
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void Start(){
        currentMission = PlayerProgress.Instance.savedPlayerData.currentMission;
    } 

    // Update is called once per frame
    void Update()
    {

    }

    public void updateMissionStatus(bool isGoalMet){
        currentMission.hasMetGoal = isGoalMet;
    }

    public void resetMission(){
        PlayerState.localPlayerData.currentMission = new PlayerData.Mission(currentMission.fireflyGoal);
    }

    // Early iteration of mission updates. Next iteration will call from an array of pre-created Missions
    // with attached buildings assets to them to represent which will be lit up
    public void assignNextMission(){
        PlayerState.localPlayerData.currentMission = new PlayerData.Mission(currentMission.fireflyGoal + 5);
    }


}
