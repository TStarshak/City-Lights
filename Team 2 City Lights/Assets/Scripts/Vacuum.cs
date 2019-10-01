using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vacuum : MonoBehaviour
{
    private ParticleSystem particles;
    public static bool isOn;
    // Start is called before the first frame update
    void Start()
    {
        particles = this.transform.GetChild(transform.childCount - 1).GetComponent<ParticleSystem>();
        isOn = false;
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

