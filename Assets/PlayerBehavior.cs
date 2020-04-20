using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public float m_speed = 1500f;
    public float directionColliderOffset = 28f;
    public Rigidbody2D m_rb2D;
    public SpriteRenderer m_spriteRenderer;
    public BoxCollider2D nextDirCollider;

    private Sprite currentSprite;
    private Vector2 currentDirection;
    private Vector2 nextDirection;

    public Vector3 lastPosition;

    /* SHOOT */
    public GameObject m_ball;
    public float m_shootDelay = 1f;
    private float m_shootDelayReaming = 0f;

    /* LIFE */
    private int life = 3;

    // Strites
    public Sprite m_leftSprite; public Sprite m_rightSprite; public Sprite m_frontSprite; public Sprite m_backSprite;

    // Start is called before the first frame update
    void Start()
    {
        this.currentSprite = m_frontSprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!nextDirCollider.IsTouchingLayers())
        {
            moveNextDirection();
        }

        if (Input.GetAxis("Horizontal") < 0f)
        {
            this.nextDirection = Vector2.left;
            this.currentSprite = m_leftSprite;
        }
        if (Input.GetAxis("Horizontal") > 0f)
        {
            this.nextDirection = Vector2.right;
            this.currentSprite = m_rightSprite;
        }
        if (Input.GetAxis("Vertical") > 0f)
        {
            this.nextDirection = Vector2.up;
            this.currentSprite = m_backSprite;
        }
        if (Input.GetAxis("Vertical") < 0f)
        {
            this.nextDirection = Vector2.down;
            this.currentSprite = m_frontSprite;
        }

        if (this.lastPosition == this.transform.position)
        {
            this.currentDirection = Vector2.zero;
        }
        else
        {
            Vector2 dir = getDirectionVector2(this.lastPosition, this.transform.position);
        }
        updateDirCollider();

        if (currentDirection == Vector2.zero)
        {
            moveNextDirection();
        }
        if (currentDirection != Vector2.zero)
        {
            this.lastPosition = this.transform.position;
            this.move(this.currentDirection);
        }

    }


    private void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.gameObject.tag == "Obstacle") {
            moveNextDirection();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision);
        if(collision.gameObject.tag == "Enemy")
        {
            life--;
            Destroy(gameObject);
        }
    }


    public void move(Vector2 vector2D)
    {
        m_rb2D.MovePosition(m_rb2D.position + Time.fixedDeltaTime * m_speed * vector2D);
    }

    public void moveNextDirection()
    {
        if (nextDirection != Vector2.zero)
        {
            this.currentDirection = this.nextDirection;
            this.m_spriteRenderer.sprite = currentSprite;
            this.nextDirection = default;
            this.move(Vector2.zero);
        }
    }

    public void updateDirCollider()
    {
        if(nextDirection != null)
        {
            nextDirCollider.offset = (Vector2.zero - nextDirection) * (-1 * directionColliderOffset);
        }
        
    }

    public Vector2 getDirectionVector2(Vector3 v1, Vector3 v2)
    {
        float x = v1.x - v2.x;
        float y = v1.y - v2.y;
        return new Vector2(x,y);
    }
}
