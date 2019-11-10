using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Economic Functionalities on Fireflies including depositing and purchases
public static class Economy
{
    /*
    Deposits the collected fireflies into the designated wallet
    */
    public static void DepositFireFlies(PlayerStatistics playerData){
        playerData.firefliesInWallet += playerData.firefliesCollected;
        Debug.Log("Deposited " + playerData.firefliesInWallet);
    }

}
