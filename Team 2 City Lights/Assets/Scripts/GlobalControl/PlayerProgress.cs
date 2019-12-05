using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
/*Stores the Global Progress of the Player Across Scenes */
public class PlayerProgress : MonoBehaviour
{
    /*
    This script uses one Singleton design meaning 
    there is one single public static instance of this class. If there is another instance,
    destroy that one and make sure that the instance is this one.
    */
    // The instance of this progress script
    public static PlayerProgress Instance;
    public bool HasVisitedCity; // Tracks whether the player has visited the city for the first time
    public bool returningFromForest; // Tracks whether the player is currently returning from the forest
    
    // References global status of player upgrades
    public PlayerUpgrades currentUpgrades = new PlayerUpgrades();

    // References global progress of player statistics
    public PlayerData savedPlayerData = new PlayerData();

    // Called upon object creation
    void Awake()
    {
        // Instantiate this tracker if none exists yet and keep game object alive
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            // loadPlayerProgress();
            Instance = this;
        }
        // Otherwise prevent duplicates
        else if (Instance != this){
            Destroy(gameObject);
        }

        HasVisitedCity = false;
        returningFromForest = false;
    }

    // Tells whether this is the player's first time in the city
    public bool firstTimeInCity(){
        return !HasVisitedCity;
    }

    // Tells whether this player is returning from the forest
    public bool isReturningFromForest(){
        return returningFromForest;
    }

    // Constructor for copying player progress
    public PlayerProgress(PlayerProgress progress){
        this.HasVisitedCity = progress.firstTimeInCity();
        this.currentUpgrades = progress.currentUpgrades;
        this.savedPlayerData = progress.savedPlayerData;
    }

    // Load progress if it exists, or create a new instance otherwise
    public void loadPlayerProgress(){
        PlayerProgress progress = SaveSystem.LoadPlayer();
        Instance = (progress == null) ? this : progress;
    }
}
