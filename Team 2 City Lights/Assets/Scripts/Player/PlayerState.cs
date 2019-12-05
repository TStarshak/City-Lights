using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statistics
    public static PlayerData localPlayerData;

    // References global status of player upgrades
    public static PlayerUpgrades currentUpgrades = new PlayerUpgrades();

    private MissionHandler.Mission currentMission;

    // At start, load data from PlayerProgress
    void OnEnable(){
        localPlayerData = new PlayerData(PlayerProgress.Instance.savedPlayerData);
    }

    void Start(){
        currentMission = MissionHandler.Instance.currentMission;
    }

    void Update(){
        // Check the player's status in accomplishing their mission
        if (localPlayerData.firefliesCollected >= currentMission.fireflyGoal){
            MissionHandler.Instance.updateMissionStatus(true);
        }
        else if (localPlayerData.firefliesCollected < currentMission.fireflyGoal){
            MissionHandler.Instance.updateMissionStatus(false);
        }
    }

    // Save Data to Global Player Progress
    public static void SavePlayer(){
        PlayerProgress.Instance.savedPlayerData = new PlayerData(localPlayerData);
        PlayerProgress.Instance.currentUpgrades = currentUpgrades;
    }

    public static void applyMultipliers(){
        localPlayerData.movementSpeed *= currentUpgrades.skillByName("speed").getMultiplier();
        localPlayerData.vacuLampCapacity *= currentUpgrades.skillByName("vlcapacity").getMultiplier();
        localPlayerData.vacuLampRange = currentUpgrades.skillByName("vlrange").getMultiplier();
    }
}
