using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{

    public Rigidbody2D m_rb2D;
    public SpriteRenderer m_spriteRenderer;
    public BoxCollider2D nextDirCollider;
    public HealthBar healthBar;

    public float m_speed = 1500f;
    public float directionColliderOffset = 28f;


    // Strites
    public Sprite m_leftSprite; public Sprite m_rightSprite; public Sprite m_frontSprite; public Sprite m_backSprite;


    private Sprite currentSprite;
    private Vector2 currentDirection;
    private Vector2 nextDirection;
    private Vector3 lastPosition;
    private int life = 3;

    private bool play = false;

    // Start is called before the first frame update
    void Start()
    {
        this.currentSprite = m_frontSprite;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(play)
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


    }

    /* EVENTS */
    private void OnTriggerExit2D(Collider2D other)
    {
        if(play)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                moveNextDirection();
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (play)
        {
            if (collision.gameObject.tag == "Enemy")
            {

                setLife(life - 1);
            }
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


    public void setLife(int life)
    {
        this.life = life;
        this.healthBar.setValue(life);
    }

    public void setPlay(bool play)
    {
        this.play = play;
    }

}
