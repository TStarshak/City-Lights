using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public float speed;
    Vector3 position;
    private Transform target;
    // Use this for initialization
    void Start()
    {
        /*
         * Find vector pointing from enemy to player
         * Save vector as point enemy will be moving towards/through
         */
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        position = this.position - target.position;
       
    }

    // Update is called once per frame
    void Update()
    {
        //Continuously move towards saved point
        transform.position = Vector2.MoveTowards(transform.position, position, speed * Time.deltaTime);

    }

    void OnCollisionEnter(collision col)
    {
        //Access player object and adjust firefly count
        //After x amount of seconds, destroy object
        Object.Destroy(this, 2.0f);
    }
}
