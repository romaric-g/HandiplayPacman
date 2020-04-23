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

    }


    public void stopLevel()
    {
        this.enemyManager.setPlay(false);
        this.player.setPlay(false);
        this.gameStats = GameStats.WAIT;
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

        this.enemyManager.setPlay(true);
        this.player.setPlay(true);
        this.gameStats = GameStats.PLAY;

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
                    Instantiate(coin, position, Quaternion.identity);
                }

            }
        }
    }
    public Grid getGrid()
    {
        return this.grid;
    }

    // Update is called once per frame

    public void respawnPlayer()
    {
        this.player.transform.position = grid.GetWorldPosition(this.respawnX, this.respawnY);
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
    }

    public void StopGame()
    {
        this.menuManager.GameOver();
        this.resetGame();
    }
}


// Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
//Debug.Log(collider.OverlapPoint((UtilsClass.GetMouseWorldPosition())));
