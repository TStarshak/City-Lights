﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    // Variables for object circling
    // private float flightRotation = 0;
    // private float flightSpeed = 10f;

    // Variables for object floating
    private float floatAmplitude = 0.25f;
    private float floatFrequency = 0.7f;
    private bool canMove;
    public bool inVac;
    private float offset;
    public Quaternion init;

    // Store the position
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
        canMove = true;
        inVac = false;
        offset = Random.value + 0.1f;
        init = transform.rotation;
        transform.Rotate(transform.up, 90 * offset);
    }

    // Update is called once per frame
    void Update()
    {
        tempPos = this.transform.position;
        // Float up and down
        tempPos.y = posOffset.y;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPos;
        // Move in a circle
        //flightRotation = (flightRotation == 360) ? 0 : flightRotation + 360 * Time.deltaTime;
        // gameObject.transform.rotation = Quaternion.Euler(0, flightRotation, 0);
        // gameObject.transform.Translate(0, 0, flightSpeed * Time.deltaTime);
        if (canMove && !inVac)
            StartCoroutine(Move());
        this.transform.rotation = init;
    }

    IEnumerator Move()
    {
        canMove = false;
        transform.Rotate(transform.up, Random.value * 15f + 15f);
        float time = Time.time;
        while(Time.time - time < 1f)
        {
            if (!inVac)
            {
                int r = (int)(Random.Range(0, 3) + 0.5f);
                if(r == 0)
                    transform.Translate(transform.forward * Time.deltaTime * 1.5f);
                if(r == 1)
                    transform.Translate(transform.right * Time.deltaTime * 1.5f);
                if (r == 2)
                    transform.Translate(-transform.forward * Time.deltaTime * 1.5f);
                if (r == 3)
                    transform.Translate(-transform.right * Time.deltaTime * 1.5f);
            }
            yield return new WaitForSeconds(0.05f);
        }
        canMove = true;
        yield return null;
    }
}
