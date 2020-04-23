using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class LevelCounter : MonoBehaviour
{
    private TextMesh textMesh;
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = UtilsClass.CreateWorldText(getText(), transform, new Vector2(0, 0), 100, Color.white, TextAnchor.MiddleRight);

    }

    // Update is called once per frame
    void Update()
    {
        textMesh.text = getText();
    }

    public string getText()
    {
        return "Level: " + gameManager.getLevel().ToString();
    }
}
