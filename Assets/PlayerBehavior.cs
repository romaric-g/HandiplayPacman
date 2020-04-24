using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayerBehavior : MonoBehaviour
{

    public GameManager gameManager;

    public Rigidbody2D m_rb2D;
    public SpriteRenderer m_spriteRenderer;
    public BoxCollider2D nextDirCollider;
    public HealthBar healthBar;

    public SoundManager soundManager;

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
    private bool changeSound = false;

    public float mouseSensi = 200f;
    private bool mouseActive = true;
    private Vector2 lastMousePosition;
    public DirectionIndicator directionIndicator;
    // Start is called before the first frame update
    void Start()
    {
        this.currentSprite = m_frontSprite;
        setMouseActive(mouseActive);
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
                changeDirection( Vector2.left );
                setMouseActive(false);
            }
            else if (Input.GetAxis("Horizontal") > 0f)
            {
                changeDirection(Vector2.right);
                setMouseActive(false);
            }
            else if (Input.GetAxis("Vertical") > 0f)
            {
                changeDirection(Vector2.up);
                setMouseActive(false);
            }
            else if (Input.GetAxis("Vertical") < 0f)
            {
                changeDirection(Vector2.down);
                setMouseActive(false);
            }
            else
            {
                Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
                if (!mouseActive && Vector2.Distance(lastMousePosition, mousePosition) > 200f)
                {
                    setMouseActive(true);
                }

                if(mouseActive)
                {
                    this.lastMousePosition = mousePosition;
                    float angle = AngleBetweenVector2(transform.position, mousePosition);
                    Debug.Log(angle);
                    if (angle > -45 && angle  < 45)
                    {
                        changeDirection(Vector2.right);
                    }
                    else if (angle > 45 && angle < 135)
                    {
                       changeDirection(Vector2.up);
                    }
                    else if (angle > 135 || angle < -135)
                    {
                       changeDirection(Vector2.left);
                    }
                    else if (angle > -135)
                    {
                      changeDirection(Vector2.down);
                    }
                    updateMouseIndicator();
                }
            }

            if (this.lastPosition == this.transform.position)
            {
                this.currentDirection = Vector2.zero;
            }
            else
            {
                Vector2 dir = getDirectionVector2(this.lastPosition, this.transform.position);
                if (changeSound)
                {
                    this.soundManager.flipSound.Play();
                    this.changeSound = false;
                }
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

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    private void setMouseActive(bool active)
    {
        mouseActive = active;
        directionIndicator.SetActive(active);
    }

    private void updateMouseIndicator()
    {
        directionIndicator.SetDirection(nextDirection);
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
                this.gameManager.respawnPlayer();
            }
        }

    }


    public void move(Vector2 vector2D)
    {
        m_rb2D.MovePosition(m_rb2D.position + Time.fixedDeltaTime * m_speed * vector2D);
    }

    public void changeDirection(Vector2 vector2)
    {
        this.nextDirection = vector2;
        if(vector2 == Vector2.up)
        {
            this.currentSprite = m_backSprite;
        }
        else if (vector2 == Vector2.right)
        {
            this.currentSprite = m_rightSprite;
        }
        else if (vector2 == Vector2.down)
        {
            this.currentSprite = m_frontSprite;
        }
        else if (vector2 == Vector2.left)
        {
            this.currentSprite = m_leftSprite;
        }
    }
    public void moveNextDirection()
    {
        if (nextDirection != Vector2.zero && this.currentDirection != this.nextDirection)
        {
            Debug.Log(this.currentDirection + " != " + this.nextDirection);
            this.currentDirection = this.nextDirection;
            this.m_spriteRenderer.sprite = currentSprite;
            this.nextDirection = default;
            this.move(Vector2.zero);
            changeSound = true;
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

    public void ResetPlayer()
    {
        setLife(3);
    }

    public void setLife(int life)
    {   
        if(this.life > life)
        {
            soundManager.deathSound.Play();
        }
        this.life = life;
        this.healthBar.setValue(life);
        if(this.life <= 0)
        {
            this.gameManager.StopGame();
        }
    }

    public void setPlay(bool play)
    {
        this.play = play;
    }

}
