using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timercontroller : MonoBehaviour
{
    public GameObject pauseM;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseM.activeInHierarchy == false) { 
            this.transform.Rotate(0, 0, 2, Space.Self);
        }
    }
}
