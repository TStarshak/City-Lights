using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statsistics
    public static PlayerStatistics localPlayerData;

    // At start, load data from PlayerProgress
    void Start(){
        localPlayerData = new PlayerStatistics(PlayerProgress.Instance.savedPlayerData);
    }

    // Save Data to Global Player Progress
    public static void SavePlayer(){
        PlayerProgress.Instance.savedPlayerData = new PlayerStatistics(localPlayerData);
    }
    void OnGUI()
    {
        //Displays a string denoting fireflies
        // string fireflyCounter = "Fireflies Collected: " + localPlayerData.firefliesCollected;
        // GUI.Box(new Rect(0, 0, Screen.width, Screen.height), fireflyCounter);
        // GUI.Box.GetComponent

    }
}
