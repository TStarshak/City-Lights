using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class CityLighting : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;                      // The currently rendered sprite for this object
    [SerializeField] private Sprite litBuildingSprite;                // The lit-up version of this building asset
    [SerializeField] private int fireflyRequirement;            // The amount of assigned fireflies required to light this building            

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (PlayerState.localPlayerData.currentMission.fireflyGoal >= fireflyRequirement){
            spriteRenderer.sprite = litBuildingSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
