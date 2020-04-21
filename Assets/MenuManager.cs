using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        mainMenu.active = false;
        gameManager.nextLevel();
    }

    public void HomeMenu()
    {
        mainMenu.active = true;
    }
}
