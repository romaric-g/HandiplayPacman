using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionCollider : MonoBehaviour
{

    public Vector2 diretion;
    public Enemy enemy;
    public BoxCollider2D collider2D;
    // Start is called before the first frame update
    void Start()
    {
        collider2D.isTrigger = true;
        BoxCollider2D enemyCollider2D = enemy.collider2D;

        collider2D.size.Set(enemyCollider2D.size.x, enemyCollider2D.size.y);
        collider2D.offset = new Vector2(enemyCollider2D.size.x, enemyCollider2D.size.y) * diretion * 0.5f;

        enemy.setDirectionAvailable(diretion, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            enemy.setDirectionAvailable(diretion, false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            enemy.setDirectionAvailable(diretion, true);
        }
    }
}
