using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgress : MonoBehaviour
{
    /*
    This script uses one Singleton design meaning 
    there is one single public static instance of this class. If there is another instance,
    destroy that one and make sure that the instance is this one.
    */
    // The instance of this progress script
    public static PlayerProgress Instance;

    // Called upon scene awake
    void Awake()
    {
        // Instantiate this tracker if none exists yet
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        // Otherwise prevent duplicates
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

}
