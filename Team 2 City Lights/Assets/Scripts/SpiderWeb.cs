using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderWeb : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerState.localPlayerData.movementSpeed /= 2f;
        }

    }
}
