using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PieceCounter : MonoBehaviour
{

    private TextMesh textMesh;
    public PieceGetter pieceGetter;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = UtilsClass.CreateWorldText(getText(), transform, new Vector2(-70, 50), 100, Color.white, TextAnchor.MiddleLeft);

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = getText();
    }

    public string getText()
    {
        return "X " + pieceGetter.getPiecesAmountGet().ToString();
    }
}
