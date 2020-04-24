using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite m_leftSprite; public Sprite m_rightSprite; public Sprite m_frontSprite; public Sprite m_backSprite;
    private Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            if (direction == Vector2.up)
            {
                this.spriteRenderer.sprite = m_frontSprite;
            }
            else if (direction == Vector2.right)
            {
                this.spriteRenderer.sprite = m_rightSprite;
            }
            else if (direction == Vector2.down)
            {
                this.spriteRenderer.sprite = m_backSprite;
            }
            else if (direction == Vector2.left)
            {
                this.spriteRenderer.sprite = m_leftSprite;
            }
        
    }

    public void SetDirection(Vector2 direction)
    {
        this.direction = direction;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
