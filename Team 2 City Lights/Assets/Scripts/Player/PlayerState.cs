using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Stores the current player data and information */
public class PlayerState : MonoBehaviour
{
    // References local progress of player statsistics
    public static PlayerStatistics localPlayerData;

    // At start, load data from PlayerProgress
    void Awake(){
        localPlayerData = new PlayerStatistics(PlayerProgress.Instance.savedPlayerData);
        Debug.Log("Collected Fireflies: " + localPlayerData.firefliesCollected);
        Debug.Log("Fireflies in Wallet: " + localPlayerData.firefliesInWallet);
    }

    // Save Data to Global Player Progress
    public static void SavePlayer(){
        Economy.DepositFireFlies(localPlayerData);
        PlayerProgress.Instance.savedPlayerData = new PlayerStatistics(localPlayerData);
    }
}
