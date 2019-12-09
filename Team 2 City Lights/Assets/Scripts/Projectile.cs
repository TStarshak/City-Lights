using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float lifeSpan = 2.0f;
    public Transform pExplosion;
    public Transform soundExplosion;
    private float projectileSpeed = 10f;
    public Transform player;
    Vector3 dir;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        dir = player.position - transform.position;
        this.GetComponent<Rigidbody>().velocity = dir.normalized * projectileSpeed;
        transform.LookAt(player);
        transform.Rotate(90, 0, 0);
    }

    void Update()
    {
        //gets the players transform

        //the direction the projectile will travel

        //Destroys the projectile after it has lived its life
        Destroy(this.gameObject, lifeSpan);

    }
}