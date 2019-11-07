﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public GameObject overlay;
    public GameObject overlaySuper;
    public static bool isPaused = false;
    public static int collectedFireflies = 0;
    // Start is called before the first frame update
    void Start()
    {

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

    void OnGUI()
    {
        // string text = "Fireflies Collected: " + FCollected;
        // GUI.Box(new Rect(0, 0, Screen.width, Screen.height), text);

    }
}
