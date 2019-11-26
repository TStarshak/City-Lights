using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of global Player numeric values. Represents a Serializable Class */
public class PlayerData
{
    public float movementSpeed;         // The player's movement speed
    public float vacuLampCapacity;      // The player's current vacu-lamp capacity
    public float vacuLampRange;         // The player's current vacu-lamp range/power
    public int firefliesCollected;    // Fireflies currently collected
    public int firefliesInWallet;     // Firelies in wallet (not deposited and for upgrade use)

    // Creates new stats with default values
    public PlayerData(){
        firefliesCollected = 0;
        firefliesInWallet = 0;
        movementSpeed = 6.0f;
        vacuLampCapacity = 20;
        vacuLampRange = 1.0f;   //Insert default range value
    }

    // Creates new stats from a copy of other stats
    public PlayerData(PlayerData oldStatistics){
        firefliesInWallet += oldStatistics.firefliesInWallet;
        firefliesCollected = oldStatistics.firefliesCollected; //Reset to prepare for next collection phase
        movementSpeed = oldStatistics.movementSpeed;
        vacuLampCapacity = oldStatistics.vacuLampCapacity;
        vacuLampRange = oldStatistics.vacuLampRange;
    }
}
