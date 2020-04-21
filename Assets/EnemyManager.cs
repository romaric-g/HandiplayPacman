using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnPoint
    {
        public int x;
        public int y;

        public SpawnPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public Enemy enemy;
    public List<SpawnPoint> spawnPoints;

    private List<Enemy> enemies = new List<Enemy>();
    private bool play = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  
    }

    public void setPlay(bool play)
    {
        this.play = play;

        foreach (Enemy enemy in enemies)
        {
            enemy.setPlay(play);
        }
    }

    public Enemy summonEnemy(Vector2 position)
    {
        Enemy gameobject = Instantiate<Enemy>(enemy, position + new Vector2(0,-5), Quaternion.identity);
        enemies.Add(gameobject);
        return enemy;
    }

    public void summonForLevel(int level, Grid grid)
    {
        Debug.Log("LEVEL/ " + level);
        for(int i = 0; i < 1 + (level/2); i++)
        {
            int spawnPointIndex = i % spawnPoints.Count;
            Debug.Log(spawnPointIndex);
            SpawnPoint spawnPoint = spawnPoints[spawnPointIndex];
            Enemy enemy = summonEnemy(grid.GetWorldPosition(spawnPoint.x, spawnPoint.y));

        }
    }

    public void clearEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
    }


}
