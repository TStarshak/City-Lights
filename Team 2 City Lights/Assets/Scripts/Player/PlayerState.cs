using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statsistics
    public static PlayerStatistics localPlayerData = new PlayerStatistics();

    // At start, load data from PlayerProgress
    void Start(){
        localPlayerData.retrieveDataFrom(PlayerProgress.Instance.savedPlayerData);
    }

    // Save Data to Global Player Progress
    public void SavePlayer(){
        PlayerProgress.Instance.savedPlayerData = localPlayerData;
    }
    void OnGUI()
    {
        string text = "Fireflies Collected: " + localPlayerData.firefliesCollected;
        GUI.Box(new Rect(0, 0, Screen.width, Screen.height), text);

    }
}
