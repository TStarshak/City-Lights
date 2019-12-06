using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{

    // Path in which the data will be saved
    private static string path = Application.persistentDataPath + "/player.fireflyter";

    // Saves the instance of the player's current progress
    public static void SavePlayer(PlayerProgress progress){
       // Creates a binary format that will save the progress data
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);

        // Save the current progress using the constructor copier
        PlayerProgress currentProgress = new PlayerProgress(progress);

        // Format the save data and save it to the designated file
        formatter.Serialize(stream, progress);
        stream.Close();
   }

    // Retrieve the saved player progress if it exists
    public static PlayerProgress LoadPlayer(){
       // If the file exists within the path...
       if (File.Exists(path)){
           // Convert the binary format and load the file
           BinaryFormatter formatter = new BinaryFormatter();
           FileStream stream = new FileStream(path, FileMode.Open);

           if (stream.Length != 0){
                PlayerProgress progress = formatter.Deserialize(stream) as PlayerProgress;
                stream.Close();
                return progress;
           }
           else {
               stream.Close();
               return null;
           }
           
       }
       else {
           // Throw an error :(
           Debug.Log("save file not found in " + path);
           return null;
       }
   }
}
