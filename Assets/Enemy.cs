using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    // Strites
    public Sprite m_leftSprite; public Sprite m_rightSprite; public Sprite m_frontSprite; public Sprite m_backSprite;

    public BoxCollider2D collider2D;
    public Rigidbody2D rigidbody2D;
    public float speed;


    private bool play = false;
    private ArrayList availableDirections = new ArrayList();
    private Vector2 currentDirection;

    private Vector3 lastPosition;
    private float positionChange = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(play)
        {
            availableDirections.Remove(currentDirection * -1);

            if (availableDirections.Count > 0)
            {
                currentDirection = (Vector2)availableDirections[Random.Range(0, availableDirections.Count)];
            }

            if (this.lastPosition == transform.position)
            {
                currentDirection = currentDirection * -1;

            }
            
            if(positionChange > 1f)
            {
                this.lastPosition = transform.position;
                positionChange = 0;
            }
            positionChange += Time.fixedDeltaTime;
            Debug.Log(Time.fixedDeltaTime);
            move(currentDirection);

        }
    }

    public void setDirectionAvailable(Vector2 vector, bool available)
    {
        if(!availableDirections.Contains(vector) && available)
        {
            availableDirections.Add(vector);
        }
        else
        {
            availableDirections.Remove(vector);
        }
    }


    public void move(Vector2 vector2D)
    {
        rigidbody2D.MovePosition(rigidbody2D.position + Time.fixedDeltaTime * speed * vector2D);
    }

    public void setPlay(bool play)
    {
        this.play = play;
    }
}
