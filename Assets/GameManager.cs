using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GameManager : MonoBehaviour
{
    public Collider2D collider;
    public GameObject coin;
    private Grid grid;

    public PlayerBehavior player;
    public int respawnX;
    public int respawnY;
    public EnemyManager enemyManager;
    public PieceGetter pieceGetter;

    public MenuManager menuManager;
    public SoundManager soundManager;

    private GameStats gameStats = GameStats.WAIT;
    private int level = 0;
    private int pieces = 0;

    private List<GameObject> coins = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(49, 29, 30f, 20f, new Vector3(-735f, -290f));

        menuManager.HomeMenu();
    }
    void Update()
    {
        if(gameStats == GameStats.PLAY && pieceGetter.getPiecesAmountGet() >= pieces)
        {
            stopLevel();
            nextLevel();
        }

        if (Input.GetKeyDown("escape"))
        {//When a key is pressed down it see if it was the escape key if it was it will execute the code
            SetPause(gameStats != GameStats.PAUSE);
        }
    }

    public void SetGameStats(GameStats stats)
    {
        this.gameStats = stats;
        bool play = stats == GameStats.PLAY;
        this.enemyManager.setPlay(play);
        this.player.setPlay(play);
    }
    public void stopLevel()
    {
        SetGameStats(GameStats.WAIT);
    }

    public void SetPause(bool pause)
    {
        if(pause && this.gameStats == GameStats.PLAY)
        {
            SetGameStats(GameStats.PAUSE);
            menuManager.PauseMenu();
        }else if(this.gameStats == GameStats.PAUSE)
        {
            SetGameStats(GameStats.PLAY);
            menuManager.closeMenus();
        }
    }
    public void StartGame()
    {
        resetGame();
        respawnPlayer();
        nextLevel();
    }
    public void nextLevel()
    {
        if(this.level > 0)this.soundManager.levelupSound.Play();
        this.level++;
        this.enemyManager.clearEnemies();
        this.enemyManager.summonForLevel(this.level, grid);

        this.pieceGetter.Reset();
        this.respawnPlayer();
        fillPieces();

        SetGameStats(GameStats.PLAY);

    }

    public int getLevel()
    {
        return this.level;
    }
    public void fillPieces()
    {
        pieces = 0;
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector2 position = grid.GetWorldPosition(x, y);
                Vector2 calcPos = position - new Vector2(grid.GetCellWidth() * 0.5f, grid.GetCellHeight() * 0.25f);

                if (!collider.OverlapPoint(calcPos))
                {
                    position -= new Vector2(grid.GetCellWidth(), grid.GetCellHeight()) * 0.5f;
                    pieces++;
                    coins.Add(Instantiate(coin, position, Quaternion.identity));
                }

            }
        }
    }

    public void clearCoins()
    {
        foreach(GameObject coin in this.coins)
        {
            Destroy(coin);
        }
        this.coins.Clear();
    }
    public Grid getGrid()
    {
        return this.grid;
    }

    // Update is called once per frame

    public void respawnPlayer()
    {
        this.player.transform.position = grid.GetWorldPosition(this.respawnX, this.respawnY);
        this.player.InitMouse();
    }
    private void HandleClickToModifyGrid()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
        }
    }

    public GameStats getStats()
    {
        return this.gameStats;
    }

    public void setStats(GameStats gameStats)
    {
        this.gameStats = gameStats;
    }

    public void resetGame()
    {
        stopLevel();
        this.player.ResetPlayer();
        this.level = 0;
        clearCoins();
    }

    public void StopGame()
    {
        this.menuManager.GameOver();
        this.resetGame();
    }
}


// Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
//Debug.Log(collider.OverlapPoint((UtilsClass.GetMouseWorldPosition())));
