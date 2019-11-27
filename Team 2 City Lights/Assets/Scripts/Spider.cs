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
            spiderMesh.SetDestination(RandomNavSphere(spawn, 18, -1, this));
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
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask, Spider spdr)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);


        if (Mathf.Abs(randomDirection.x / randomDirection.z) < Mathf.Tan(30) && randomDirection.z < 0)
            spdr.anim.SetTrigger("Backwalk");
        if (Mathf.Abs(randomDirection.x / randomDirection.z) > Mathf.Tan(30) && Mathf.Abs(randomDirection.x / randomDirection.z) < Mathf.Tan(75) && randomDirection.z < 0)
            spdr.anim.SetTrigger("BackSide");
        if (Mathf.Abs(randomDirection.x / randomDirection.z) > Mathf.Tan(75))
            spdr.anim.SetTrigger("Side");
        if (Mathf.Abs(randomDirection.x / randomDirection.z) < Mathf.Abs(Mathf.Tan(105)) && Mathf.Abs(randomDirection.x / randomDirection.z) > Mathf.Abs(Mathf.Tan(165)) && randomDirection.z > 0)
            spdr.anim.SetTrigger("Frontside");
        if (Mathf.Abs(randomDirection.x / randomDirection.z) < Mathf.Abs(Mathf.Tan(165)) && randomDirection.z > 0)
            spdr.anim.SetTrigger("Front");

        spdr.rend.flipX = randomDirection.x < 0 ? false : true;

        return navHit.position;
    }


}
