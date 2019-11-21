using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerFlireflyController : MonoBehaviour
{
    private Sprite[] fireflySprites;
    int x = 0;
    int spacer = 0;
    // Start is called before the first frame update
    void Start()
    {
        fireflySprites = Resources.LoadAll<Sprite>("UI/fireflyloopr");
        this.gameObject.GetComponent<Image>().sprite = fireflySprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Image>().sprite = fireflySprites[x];
        spacer++;
        if (spacer==5) {
            spacer = 0;
            x++;
            if (x == 44)
            {
                x = 0;
            }
        }
    }
}
