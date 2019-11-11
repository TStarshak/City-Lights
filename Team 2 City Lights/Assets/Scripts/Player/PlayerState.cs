﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statistics
    public static PlayerStatistics localPlayerData;

    private MissionHandler.Mission currentMission;

    // At start, load data from PlayerProgress
    void Awake(){
        localPlayerData = new PlayerStatistics(PlayerProgress.Instance.savedPlayerData);
        currentMission = MissionHandler.Instance.currentMission;
    }

    void Update(){
        // Check the plkayer's status in accomplishing their mission
        if (localPlayerData.firefliesCollected >= currentMission.fireflyGoal){
            MissionHandler.Instance.updateMissionStatus(true);
        }
        else if (localPlayerData.firefliesCollected < currentMission.fireflyGoal){
            MissionHandler.Instance.updateMissionStatus(false);
        }
    }

    // Save Data to Global Player Progress
    public static void SavePlayer(){
        Economy.DepositFireFlies(localPlayerData);
        PlayerProgress.Instance.savedPlayerData = new PlayerStatistics(localPlayerData);
    }
}
