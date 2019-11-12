using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of global Player numeric values. Represents a Serializable Class */
public class PlayerStatistics
{
    public float movementSpeed;         // The player's movement speed
    public int firefliesCollected;    // Fireflies currently collected
    public int firefliesInWallet;     // Firelies in wallet (not deposited and for upgrade use)
    public int vacuLampCapacity;      // The player's current vacu-lamp capacity

    // Creates new stats with default values
    public PlayerStatistics(){
        firefliesCollected = 0;
        firefliesInWallet = 0;
        movementSpeed = 6.0f;
        vacuLampCapacity = 20;
    }

    // Creates new stats from a copy of other stats
    public PlayerStatistics(PlayerStatistics oldStatistics){
        firefliesInWallet += oldStatistics.firefliesInWallet;
        firefliesCollected = oldStatistics.firefliesCollected; //Reset to prepare for next collection phase
        movementSpeed = oldStatistics.movementSpeed;
        vacuLampCapacity = oldStatistics.vacuLampCapacity;
    }
}
