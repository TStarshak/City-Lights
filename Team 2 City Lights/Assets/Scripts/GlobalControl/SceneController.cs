using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Global Script for Scene Management and Navigation */
public class SceneController : MonoBehaviour
{

    public static SceneController Instance;

    public GameObject overlay;
    public GameObject overlaySuper;
    public static bool isPaused = false;

    // Called upon object creation
    void Awake()
    {
        // Instantiate this tracker if none exists yet and keep game object alive
        if (Instance == null){
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        // Otherwise prevent duplicates
        else if (Instance != this){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (overlaySuper.activeInHierarchy)
            {
                overlaySuper.SetActive(false);
            }
            else
            {
                if (overlay.activeInHierarchy)
                {
                    overlay.SetActive(false);
                    isPaused = true;
                }
                else
                {
                    overlay.SetActive(true);
                    isPaused = false;
                }
            }
        }
    }
}
