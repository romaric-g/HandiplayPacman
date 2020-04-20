using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{

    public PlayerBehavior playerBehavior;
    public SpriteRenderer spriteRenderer;

    public List<Sprite> sprites;

    private int value = 3;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(value >= 0 && value < sprites.Count)
        {
            spriteRenderer.sprite = sprites[value];
        }
    }


    public void setValue(int value)
    {
        this.value = value;
    }


}
