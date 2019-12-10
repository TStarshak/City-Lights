using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool returningFromForest; // Tracks whether the player is currently returning from the forest
    
    // References global status of player upgrades
    public PlayerUpgrades currentUpgrades = new PlayerUpgrades();

    // References global progress of player statistics
    public PlayerData savedPlayerData = new PlayerData();

    // Helper boolean to be toggled in editor menu for erasing saved player data
    [SerializeField] private bool resetData;

    // Called upon object creation
    void Awake()
    {
        // Instantiate this tracker if none exists yet and keep game object alive
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
            loadPlayerProgress();
        }
        // Otherwise prevent duplicates
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

    void OnEnable(){
        returningFromForest = false;
    }

    // Tells whether this is the player's first time in the city
    public bool firstTimeInCity(){
        return !savedPlayerData.HasVisitedCity;
    }

    // Tells whether this player is returning from the forest
    public bool isReturningFromForest(){
        return returningFromForest;
    }

    // Load progress if it exists, or create a new instance otherwise
    public void loadPlayerProgress(){
        // Call for data erase if desired
        if (resetData){
            SaveSystem.DeleteSavedData();
            savedPlayerData.firefliesCollected = 0;
            resetData = false;
        }
        else if (SaveSystem.playerSaveExists()){
            SaveSystem.LoadPlayer(this);
        }
    }

    // Reset the player's progress
    public void resetPlayerProgress(){
        returningFromForest = false;
        currentUpgrades = new PlayerUpgrades();
        savedPlayerData = new PlayerData();
    }
}
