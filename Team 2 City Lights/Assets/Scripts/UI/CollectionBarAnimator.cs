using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectionBarAnimator : MonoBehaviour
{
    public Sprite[] barSprites;
    int x = 0;
    // Start is called before the first frame update
    void Start()
    {
        barSprites = Resources.LoadAll<Sprite>("UI/collectionBar");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.GetComponent<Image>().sprite = barSprites[x];
        x++;
        if (x==144)
        {
            x = 0;
        }
    }
}
