﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statistics
    public static PlayerData localPlayerData;

    // References global status of player upgrades
    public static PlayerUpgrades currentUpgrades;
    public static bool isInDangerState;            // Bool to denote if the player is a hit from losing (red screen, camera shake)

    // At start, load data from PlayerProgress
    void OnEnable(){
        localPlayerData = new PlayerData(PlayerProgress.Instance.savedPlayerData);
        currentUpgrades = new PlayerUpgrades(PlayerProgress.Instance.currentUpgrades);
    }

    void Start()
    {
        isInDangerState = false;
    }

    void Update()
    {
        // Check the player's status in accomplishing their mission
        if (localPlayerData.firefliesCollected >= localPlayerData.currentMission.fireflyGoal)
        {
            MissionHandler.Instance.updateMissionStatus(true);
        }
        else if (localPlayerData.firefliesCollected < localPlayerData.currentMission.fireflyGoal)
        {
            MissionHandler.Instance.updateMissionStatus(false);
        }
    }

    // Save Data to Global Player Progress
    public static void SavePlayer()
    {
        PlayerProgress.Instance.savedPlayerData = new PlayerData(localPlayerData);
        PlayerProgress.Instance.currentUpgrades = currentUpgrades;
    }

    public static void applyMultipliers()
    {
        localPlayerData.movementSpeed *= currentUpgrades.skillByName("speed").getMultiplier();
        localPlayerData.vacuLampCapacity *= currentUpgrades.skillByName("vlcapacity").getMultiplier();
        localPlayerData.vacuLampRange = currentUpgrades.skillByName("vlrange").getMultiplier();
    }

    public static void playerDeath()
    {
        Animator anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        anim.SetTrigger("Death");
        //After playing the animation, move to the city
        Debug.Log("DEATH");
        
    }

    public static void dangerState()
    {
        isInDangerState = true;
        //Camera Shake

        //Turn Edge of player red

    }
}
