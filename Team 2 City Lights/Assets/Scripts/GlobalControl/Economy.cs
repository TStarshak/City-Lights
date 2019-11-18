using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Economic Functionalities on Fireflies including depositing and purchases
public static class Economy
{
    /*
    Deposits the collected fireflies into the designated wallet
    */
    public static void DepositFireFlies(PlayerData player){
        if(MissionHandler.Instance.currentMission.hasMetGoal){
            player.firefliesInWallet += player.firefliesCollected - MissionHandler.Instance.currentMission.fireflyGoal;
        }
        else {
            player.firefliesInWallet += player.firefliesCollected;
        }
    }

    public static void PurchaseSkill(PlayerData player, PlayerUpgrades.Skill skill){
        player.firefliesInWallet -= skill.getUpgradeCost();
        skill.upgradeLevel();
    }

    public static bool CanAffordSkill(PlayerData playerData, PlayerUpgrades.Skill skill){
        return playerData.firefliesInWallet >= skill.getUpgradeCost();
    }

}
