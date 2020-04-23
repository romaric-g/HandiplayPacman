using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public SoundManager soundManager;
    public GameObject mainMenu;
    public GameObject gameoverMenu;

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
        
        soundManager.buttonSound.Play();
        soundManager.menuMusic.Stop();
        soundManager.gameMusic.Play();
        this.closeMenus();
        gameManager.StartGame();
    }

    public void HomeMenu()
    {
        this.closeMenus();
        soundManager.menuMusic.Play();
        mainMenu.active = true;
    }

    public void GameOver()
    {
        this.closeMenus();
        gameoverMenu.active = true;
    }

    public void closeMenus()
    {
        mainMenu.active = false;
        gameoverMenu.active = false;
    }
}
