using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Keeps track of global Player numeric values. Represents a Serializable Class */
public class PlayerStatistics
{
    public float firefliesCollected;    // Fireflies currently collected
    public float firefliesInWallet;     // Firelies in wallet (not deposited and for upgrade use)
    public float movementSpeed;         // The player's movement speed

    // Creates new stats with default values
    public PlayerStatistics(){
        firefliesCollected = 0;
        firefliesInWallet = 0;
        movementSpeed = 10.0f;
    }

    // Creates new stats from a copy of other stats
    public PlayerStatistics(PlayerStatistics oldStatistics){
        firefliesCollected = oldStatistics.firefliesCollected;
        firefliesInWallet = oldStatistics.firefliesInWallet;
        movementSpeed = oldStatistics.movementSpeed;
    }
}
