using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penemy : MonoBehaviour
{
    [SerializeField] private GameObject Projectile;
    public float secondsBetweenSpawn;
    public float elapsedTime = 0.0f;

    // Start is called before the first frame update
    void Update()
    {
        //timer for spawn
        elapsedTime += Time.deltaTime;

        if (elapsedTime > secondsBetweenSpawn)
        {
            elapsedTime = 0;
            Instantiate(Projectile);
        }
       
    }

}
