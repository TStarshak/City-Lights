using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRock : MonoBehaviour
{
    void Update()
    {
        if (Physics.CheckBox(new Vector3(this.transform.position.x - 0.2f, this.transform.position.y + 0.44f, this.transform.position.z + 0.48f), new Vector3(1.63f, 0.78f, 2.295f), new Quaternion(0f, 0f, 0f, 0f)))
        {
            Debug.Log("COLLISION");
        } else
        {
            Debug.Log("NO COLLISION");
        }
    }
}
