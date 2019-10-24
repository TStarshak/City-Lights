﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : MonoBehaviour
{
    [SerializeField] private GameObject gameLightInner;
    [SerializeField] private GameObject gameLightOuter;
    [SerializeField] private GameObject gameLightPlayer;

    public static int capacity;
    public static int numFireflies;

    private static float lightRange = 0.05f;
    private float lightTrans = lightRange / 5;

    // Start is called before the first frame update
    void Start()
    {
        capacity = 10;
        numFireflies = 0;
       
        gameLightInner.transform.SetPositionAndRotation(new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));
        gameLightOuter.transform.SetPositionAndRotation(new Vector3(0, 4, 0), new Quaternion(0, 0, 0, 0));

        gameLightInner.GetComponent<Light>().range = 5;
        gameLightOuter.GetComponent<Light>().range = 7;

        gameLightInner.GetComponent<Light>().intensity = 1.5f;
        gameLightOuter.GetComponent<Light>().intensity = 1f;
        gameLightPlayer.GetComponent<Light>().intensity = 1f;

        gameLightInner.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.2941177f);
        gameLightOuter.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.2941177f);
        gameLightPlayer.GetComponent<Light>().color = new Color(0.9921569f, 0.9254902f, 0.4941177f);

        gameLightInner.GetComponent<Light>().shadows = LightShadows.Soft;
        gameLightOuter.GetComponent<Light>().shadows = LightShadows.Soft;
        gameLightPlayer.GetComponent<Light>().shadows = LightShadows.Soft;
        
    }
    // Update is called once per frame
    void Update()
    { /*
        if (Input.GetKeyDown(KeyCode.LeftShift))
            StartCoroutine(SmoothLight(lightRange));
        if (Input.GetKeyDown(KeyCode.R) && gameLightInner.GetComponent<Light>().range > 3)
            StartCoroutine(SmoothLight(-lightRange));  */
    }

    void onFireflyEnter()
    {
        Debug.Log(Lighting.numFireflies);
        numFireflies++;
        sceneController.FCollected++;
        StartCoroutine(SmoothLight(lightRange));
    }

    void onEnemyEnter()
    {
        Debug.Log(Lighting.numFireflies);
        if (numFireflies > 0)
        {
            numFireflies--;
            sceneController.FCollected--;
        }
        if(gameLightInner.GetComponent<Light>().range > 3)
         StartCoroutine(SmoothLight(-lightRange));
    }

    IEnumerator SmoothLight(float range)
    {
        Light lInner = gameLightInner.GetComponent<Light>();
        Light lOuter = gameLightOuter.GetComponent<Light>();
        Light lPlayer = gameLightPlayer.GetComponent<Light>();
        if ((lPlayer.intensity > 1 && range < 0) || (lPlayer.intensity < 2.4 && range > 0))
            if (range > 0)
                lPlayer.intensity += 0.2f;
            else
                lPlayer.intensity -= 0.2f;
        float time = Time.time;
        while (Time.time - time < 0.5f)
        {
            lInner.range += range;
            lInner.transform.Translate(new Vector3(0, (lightTrans * range / Mathf.Abs(range)), 0));
            lOuter.range += (1.5f * range);
            lOuter.transform.Translate(new Vector3(0, (1.5f * lightTrans * range / Mathf.Abs(range)), 0));
            yield return null;
        }
    }
}