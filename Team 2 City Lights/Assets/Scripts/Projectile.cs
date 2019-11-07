using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    private float lifeSpan = 2.0f;
    public Transform pExplosion;
    public Transform soundExplosion;
    private float projectileSpeed = 10f;
    public Transform player;


    void Update()
    {
        //gets the players transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //the direction the projectile will travel
        Vector3 dir = player.position - transform.position;
        this.GetComponent<Rigidbody>().velocity = dir.normalized * projectileSpeed;
        //Destroys the projectile after it has lived its life
        Destroy(this.gameObject, lifeSpan);

    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Enemy")
        {
            if (other.gameObject.tag == "Player") ;
            {
                Destroy(this.gameObject);

            }
        }
    }

}