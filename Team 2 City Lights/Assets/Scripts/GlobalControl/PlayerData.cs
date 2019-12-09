using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
/* Keeps track of global Player numeric values. Represents a Serializable Class */
public class PlayerData
{
    public float movementSpeed;         // The player's movement speed
    public float vacuLampCapacity;      // The player's current vacu-lamp capacity
    public float vacuLampRange;         // The player's current vacu-lamp range/power
<<<<<<< HEAD
    public int firefliesCollected;    // Fireflies currently collected
    public int firefliesInWallet;     // Firelies in wallet (not deposited and for upgrade use)
    public bool inDangerState;         //Bool to denote if the player is a hit from losing (red screen, camera shake)
    public bool isDead;
=======
    public int firefliesCollected;      // Fireflies currently collected
    public int firefliesInWallet;       // Firelies in wallet (not deposited and for upgrade use)
    public bool HasVisitedCity;         // Tracks whether the player has visited the city for the first time
    public Mission currentMission;      // The current mission that the player is on
>>>>>>> d943178c35f025c23f4cfbefcdb26bc6fed93fe5


    // A subclass for a mission to be assigned by the Fireflyter Chief
    [System.Serializable]
    public class Mission{

        public int fireflyGoal;
        public bool hasMetGoal;

        public Mission(int newGoal){
            // Set the Default firefly goal
            fireflyGoal = newGoal;
            // Tracks whether the player has met thee goal for this mission
            hasMetGoal = false;
        }

    }
    
    // Creates new stats with default values
    public PlayerData(){
        firefliesCollected = 0;
        firefliesInWallet = 0;
        movementSpeed = 6.0f;
        vacuLampCapacity = 20;
        vacuLampRange = 1.0f;   //Insert default range value
        HasVisitedCity = false;
        currentMission = new Mission(10);
    }

    // Creates new stats from a copy of other stats
    public PlayerData(PlayerData oldStatistics){
        firefliesInWallet += oldStatistics.firefliesInWallet;
        firefliesCollected = oldStatistics.firefliesCollected; //Reset to prepare for next collection phase
        movementSpeed = oldStatistics.movementSpeed;
        vacuLampCapacity = oldStatistics.vacuLampCapacity;
        vacuLampRange = oldStatistics.vacuLampRange;
<<<<<<< HEAD
        inDangerState = false;
        isDead = false;
=======
        HasVisitedCity = oldStatistics.HasVisitedCity;
        currentMission = oldStatistics.currentMission;
>>>>>>> d943178c35f025c23f4cfbefcdb26bc6fed93fe5
    }
}
