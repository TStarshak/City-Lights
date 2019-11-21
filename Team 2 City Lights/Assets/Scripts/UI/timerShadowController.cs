using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerShadowController : MonoBehaviour
{
    private Sprite[] shadowSprites;
    int x = 0;
    int spacer = 0;
    // Start is called before the first frame update
    void Start()
    {
        shadowSprites = Resources.LoadAll<Sprite>("UI/shadowhour looop");
        this.gameObject.GetComponent<Image>().sprite = shadowSprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Image>().sprite = shadowSprites[x];
        spacer++;
        if (spacer == 2)
        {
            spacer = 0;
            x++;
            if (x == 50)
            {
                x = 0;
            }
        }
    }
}
