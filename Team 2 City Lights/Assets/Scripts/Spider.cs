using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : MonoBehaviour
{
    private NavMeshAgent spiderMesh;
    public float secondsBetweenSpawn;
    private float elapsedTime = 0.0f;
    [SerializeField] private GameObject web;
    private float numWebs = 0;
    private Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spiderMesh = GetComponent<NavMeshAgent>();
        spawn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            spiderMesh.SetDestination(RandomNavSphere(spawn, 18, -1));
            elapsedTime = 0;
            if (numWebs <= 5)
            {
                Instantiate(web, transform.position, transform.rotation);
                numWebs++;
            }
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }


}
