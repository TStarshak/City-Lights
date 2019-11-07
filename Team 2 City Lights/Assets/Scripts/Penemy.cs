using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Penemy : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;
    public float secondsBetweenSpawn;
    private float elapsedTime = 0.0f;
    public GameObject Player;
    public float activationDistance = 5.0f;
    private NavMeshAgent pMesh;

    private void Start()
    {
        pMesh = GetComponent<NavMeshAgent>(); //applies the agent to our lovely enemy
    }
    // Start is called before the first frame update
    void Update()
    {
        //timer for spawn
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn) 
        {
            elapsedTime = 0;
            Instantiate(Projectile, transform.position, transform.rotation );
        }

        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance > activationDistance)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;

            Vector3 newPos = transform.position - dirToPlayer;

            pMesh.SetDestination(newPos);

        }
        else pMesh.SetDestination(transform.position);
    }

}


