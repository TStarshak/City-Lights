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
    private Quaternion rot;
    private SpriteRenderer rend;
    private bool lookRight;
    private bool lookFront;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rot = this.transform.rotation;
        spiderMesh = GetComponent<NavMeshAgent>();
        spawn = transform.position;

        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        lookRight = true;
        lookFront = true;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        this.transform.rotation = rot;
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
        if (transform.forward.x > 0 && lookRight)
        {
            lookRight = false;
            rend.flipX = true;
        }
        else if (transform.forward.x < 0 && !lookRight)
        {
            lookRight = true;
            rend.flipX = false;
        }
        anim.SetFloat("X", transform.forward.x);
        anim.SetFloat("Y", transform.forward.z);
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
