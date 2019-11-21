using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionBarController : MonoBehaviour
{
    public float x = 1.0f;
    //public float w = this.gameObject.rect.width;
    public Vector3 s = new Vector3();
    public float goal = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        s = this.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        x = s.x * (1.0f/goal) * sceneController.FCollected;
        if (x > s.x)
        {
            x = s.x;
        }
        this.gameObject.transform.localScale = s - new Vector3(x , 0 , 0);
    }
}
