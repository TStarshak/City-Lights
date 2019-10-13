using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent Shadoow; //Calls the mesh agent that tells our enemy what paths are travesable
    public GameObject Player;
    public float activationDistance = 3.0f; //This distance determines how close you have to get to make the enemy chase after you
    private Quaternion rot;
    private SpriteRenderer rend;
    private bool lookRight;
    private Light eyes;
    private Animator anim;

    void Start()
    {
        rot = transform.rotation;
        Shadoow=GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        eyes = GetComponentInChildren<Light>();
        lookRight = true;
    }

    void Update() //this all just tells the enemy to chase the player
    {
        float distance=Vector3.Distance(transform.position, Player.transform.position);
        Shadoow.transform.rotation = rot;
        if (distance < activationDistance)
        {
            eyes.intensity = 1;
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPos = transform.position - dirToPlayer;
            Shadoow.SetDestination(newPos);
            float xPos = dirToPlayer.x;
            if (xPos > 0 && lookRight)
            {
                lookRight = false;
                rend.flipX = true;
            }
            else if (xPos < 0 && !lookRight)
            {
                lookRight = true;
                rend.flipX = false;
            }
        }
        else
            eyes.intensity = 0;
    }
}
