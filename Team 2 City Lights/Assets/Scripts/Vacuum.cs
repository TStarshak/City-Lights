﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    [SerializeField] private GameObject vacuum;
    private ParticleSystem particles;
    public static bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        particles = this.transform.GetChild(transform.childCount - 1).GetComponent<ParticleSystem>();
        isOn = false;
        transform.GetChild(transform.childCount - 1).transform.Rotate(new Vector3(0, 1, 0), 90);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            particles.Play();
            isOn = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            particles.Pause();
            particles.Clear();
            isOn = false;
        }
        Plane playerPlane = new Plane(Vector3.up, transform.position);
       // Vector3 vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);// -this.transform.GetChild(transform.childCount - 1).transform.position;
       // vec.y = this.transform.GetChild(transform.childCount - 1).transform.position.y;

       // this.transform.GetChild(transform.childCount - 1).transform.LookAt(vec);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.

        if (playerPlane.Raycast(ray, out hitdist))
        {
            Vector3 targetPoint = ray.GetPoint(hitdist);
            targetPoint.y = transform.GetChild(transform.childCount - 1).transform.position.y;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.GetChild(transform.childCount - 1).transform.position);
            Vector3 rot = transform.GetChild(transform.childCount - 1).transform.rotation.eulerAngles - new Vector3(0, 90, 0);
            transform.GetChild(transform.childCount - 1).transform.rotation = Quaternion.Lerp(transform.GetChild(transform.childCount - 1).transform.rotation, targetRotation, 10.0f * Time.deltaTime);
            
        }


    }

    IEnumerator moveFirefly(GameObject firefly)
    {
        float xComp = 0;
        float zComp = 0;
        if (firefly.transform.position.x > this.transform.position.x)
            xComp = -0.1f;
       else
            xComp = 0.1f;
       if (firefly.transform.position.z > this.transform.position.z)
            zComp = -0.1f;
       else
            zComp = 0.1f;

        firefly.transform.Translate(xComp, 0, zComp);
        yield return null;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.Equals("Firefly") && isOn)
            StartCoroutine(moveFirefly(other.gameObject));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Firefly") && isOn)
            StartCoroutine(moveFirefly(other.gameObject));
    }
}

