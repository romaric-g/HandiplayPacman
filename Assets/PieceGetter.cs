using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGetter : MonoBehaviour
{

    public SoundManager soundManager;
    private int pieceGet = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "coin")
        {
            Destroy(collider.gameObject);
            soundManager.coinSound.Play();
            pieceGet++;
        }
    }

    public int getPiecesAmountGet()
    {
        return pieceGet;
    }

    public void Reset()
    {
        this.pieceGet = 0;        
    }
}
