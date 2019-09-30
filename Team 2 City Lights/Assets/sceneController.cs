using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sceneController : MonoBehaviour
{
    public GameObject overlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            if (overlay.activeInHierarchy)
            {
                overlay.SetActive(false);

            }
            else
            {
                overlay.SetActive(true);
            }

        }
    }
}
