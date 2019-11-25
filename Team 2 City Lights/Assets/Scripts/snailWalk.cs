﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snailWalk : MonoBehaviour
{
    public GameObject mushroom;
    public float currentAngle;
    private Vector3 nextPos;
    private bool direction = true;
    public float rotateAngle = 0.8f;

    void Start()
    {
        if (mushroom == null)
        {
            Destroy(this);
        }
        StartCoroutine(walk());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("mushroom(Clone)") || other.gameObject.name.Equals("Mesh_group1") || other.gameObject.name.Equals("Mesh"))
        {
            if (direction)
            {
                direction = false;
                transform.Rotate(0, 180, 0);
            }
            else
            {
                direction = true;
                transform.Rotate(0, 180, 0);
            }
        }
    }

    private IEnumerator walk()
    {
        while (true)
        {
            if (direction && currentAngle > 360f)
            {
                currentAngle = 0.01f;
            } else if(direction)
            {
                currentAngle += 0.01f;
            } else if (!direction && currentAngle < 0f)
            {
                currentAngle = 359.99f;
            }
            else
            {
                currentAngle -= 0.01f;
            }

            nextPos = new Vector3(mushroom.transform.position.x + (0.9f * Mathf.Cos(currentAngle)), transform.position.y, mushroom.transform.position.z + (0.9f * Mathf.Sin(currentAngle)));
            transform.position = nextPos;
            transform.eulerAngles = new Vector3(transform.rotation.x, currentAngle, transform.rotation.z);


            RaycastHit hit;
            if (direction)
            {
                Physics.Raycast(new Vector3(mushroom.transform.position.x + (0.9f * Mathf.Cos(currentAngle + 0.3f)), transform.position.y + 0.5f, mushroom.transform.position.z + (0.9f * Mathf.Sin(currentAngle + 0.3f))), -Vector3.up, out hit);
            }
            else
            {
                Physics.Raycast(new Vector3(mushroom.transform.position.x + (0.9f * Mathf.Cos(currentAngle - 0.3f)), transform.position.y + 0.5f, mushroom.transform.position.z + (0.9f * Mathf.Sin(currentAngle - 0.3f))), -Vector3.up, out hit);
            }

            if (hit.collider == null)
            {
                if (direction)
                {
                    direction = false;
                    transform.Rotate(0, 180, 0);
                }
                else
                {
                    direction = true;
                    transform.Rotate(0, 180, 0);
                }
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

}