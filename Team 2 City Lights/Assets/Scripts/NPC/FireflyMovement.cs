using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyMovement : MonoBehaviour
{
    // Variables for object circling
    // private float flightRotation = 0;
    // private float flightSpeed = 10f;

    // Variables for object floating
    private float floatAmplitude = 0.5f;
    private float floatFrequency = 1f;

    // Store the position
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Float up and down
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * floatFrequency) * floatAmplitude;
        transform.position = tempPos;
        // Move in a circle
        // flightRotation = (flightRotation == 360) ? 0 : flightRotation + 360 * Time.deltaTime;
        // gameObject.transform.rotation = Quaternion.Euler(0, flightRotation, 0);
        // gameObject.transform.Translate(0, 0, flightSpeed * Time.deltaTime);
    }
}
