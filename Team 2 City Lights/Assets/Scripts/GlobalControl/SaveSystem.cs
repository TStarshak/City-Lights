using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class SaveSystem
{
    // Path in which the data will be saved
    private static string savePath = Application.persistentDataPath + "/gamesave.save";

    // Class representing an object of saved player data to be saved/loaded
    [System.Serializable]
    public class SaveData{
        public PlayerData savedPlayerData;
        public PlayerUpgrades savedUpgrades;

        public SaveData(PlayerData playerData, PlayerUpgrades upgrades){
            savedPlayerData = playerData;
            savedUpgrades = upgrades;
        }
    }

    // Saves the instance of the player's current progress
    public static void SavePlayer(PlayerData playerData, PlayerUpgrades upgrades){
       // Creates a binary format that will save the progress data
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(savePath);
        // Save the current progress using the constructor copier
        SaveData saveData = new SaveData(new PlayerData(playerData), new PlayerUpgrades(upgrades));

        // Format the save data and save it to the designated file
        formatter.Serialize(file, saveData);
        file.Close();
   }

    // Retrieve the saved player progress if it exists
    public static void LoadPlayer(PlayerProgress currentProgress){
       // If the file exists within the path...
       if (playerSaveExists()){
           // Convert the binary format and load the file
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream file = File.Open(savePath, FileMode.Open);
           // Only load if the save file isn't empty
           SaveData saveData = formatter.Deserialize(file) as SaveData;
           file.Close();
           currentProgress.savedPlayerData = saveData.savedPlayerData;
           currentProgress.currentUpgrades = saveData.savedUpgrades;
        }   
        // Throw an error and return null if there was nothing to load :(
        Debug.Log("save file not found in " + savePath);
   }

    // Deletes any existing at the save path (currently used for debugging)
   public static void DeleteSavedData(){
       if (playerSaveExists()){
           File.Delete(savePath);
       }
       else {
           Debug.Log("Save File does not exist");
       }
   }

    // Returns whether there exists a saved player data within the file path
   public static bool playerSaveExists(){
       if (File.Exists(savePath)){
           FileStream file = File.Open(savePath, FileMode.Open);
           bool fileExists = file.Length != 0;
           file.Close();
           return fileExists;
       }
       return false;
   }
}
