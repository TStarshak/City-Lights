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

    // References global progress of player statsistics
    public PlayerStatistics savedPlayerData = new PlayerStatistics();

    // Called upon object creation
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
        setDefaultAttributes();
    }

    private void setDefaultAttributes(){
        savedPlayerData.movementSpeed = 10.0f;
    }

}
