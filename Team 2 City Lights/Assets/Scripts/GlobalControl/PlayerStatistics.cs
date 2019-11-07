using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of global Player numeric values. Represents a Serializable Class */
public class PlayerStatistics
{
    public float firefliesCollected;    // Fireflies currently collected
    public float firefliesInWallet;     // Firelies in wallet (not deposited and for upgrade use)
    public float movementSpeed;         // The player's movement speed

    // Copies values from another instance of player data
    public void retrieveDataFrom(PlayerStatistics otherStats){
        firefliesCollected = otherStats.firefliesCollected;
        firefliesInWallet = otherStats.firefliesInWallet;
        movementSpeed = otherStats.movementSpeed;
    }
}
