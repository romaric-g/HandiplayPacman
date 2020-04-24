using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    public GameManager gameManager;
    public SoundManager soundManager;
    public GameObject mainMenu;
    public GameObject gameoverMenu;
    public GameObject pauseMenu;

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

    public void ResumeGame()
    {
        soundManager.buttonSound.Play();
        gameManager.SetPause(false);
    }

    public void QuitGame()
    {
        soundManager.buttonSound.Play();
        gameManager.StopGame();
        HomeMenu();
    }
    public void HomeMenu()
    {
        this.closeMenus();
        soundManager.gameMusic.Stop();
        soundManager.menuMusic.Play();
        mainMenu.active = true;
    }

    public void GameOver()
    {
        this.closeMenus();
        gameoverMenu.active = true;
    }

    public void PauseMenu()
    {
        this.closeMenus();
        pauseMenu.active = true;
    }
    public void closeMenus()
    {
        mainMenu.active = false;
        gameoverMenu.active = false;
        pauseMenu.active = false;
    }

}
