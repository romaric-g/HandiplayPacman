using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float m_speed = 1000f;
    public Rigidbody2D m_rb2D;
    public float m_lifeRemaining = 0f;
    public Vector2 m_shootDirection;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_shootDirection != null)
        {
            m_rb2D.MovePosition(m_rb2D.position + Time.fixedDeltaTime * m_speed * m_shootDirection);
            m_lifeRemaining = m_lifeRemaining + Time.fixedDeltaTime;
        }

        if (m_lifeRemaining > 1f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "BoxWood")
        {
            Destroy(collision.gameObject);
        }

    }
}