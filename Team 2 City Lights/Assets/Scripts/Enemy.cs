using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    private NavMeshAgent Shadoow; //Calls the mesh agent that tells our enemy what paths are travesable
    public GameObject Player;
    public float activationDistance = 3.0f; //This distance determines how close you have to get to make the enemy chase after you
    public GameObject SceneC;

    void Start()
    {
        Shadoow = GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
    }

    void Update() //this all just tells the enemy to chase the player
    {
        if (SceneC.activeInHierarchy == false) {
            float distance = Vector3.Distance(transform.position, Player.transform.position);

            if (distance < activationDistance)
            {
                Vector3 dirToPlayer = transform.position - Player.transform.position;

                Vector3 newPos = transform.position - dirToPlayer;

                Shadoow.SetDestination(newPos);
            }
        }
    }
}