using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class GridSystem : MonoBehaviour
{
    public Collider2D collider;
    public GameObject coin;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = new Grid(49, 29, 30f, 20f, new Vector3(-735f, -290f));
        for (int x = 0; x < grid.GetWidth(); x++)
        {
            for (int y = 0; y < grid.GetHeight(); y++)
            {
                Vector2 position = grid.GetWorldPosition(x, y);
                Vector2 calcPos = position - new Vector2(grid.GetCellWidth() * 0.5f, grid.GetCellHeight() * 0.25f);

                if (!collider.OverlapPoint(calcPos) )
                {
                    position -= new Vector2(grid.GetCellWidth(), grid.GetCellHeight()) * 0.5f;
                    Instantiate(coin, position, Quaternion.identity);
                }
                
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
        }
    }

    public Grid getGrid()
    {
        return this.grid;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(UtilsClass.GetMouseWorldPosition()));
            Debug.Log(collider.OverlapPoint((UtilsClass.GetMouseWorldPosition())));
        }
    }

    private void HandleClickToModifyGrid()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(UtilsClass.GetMouseWorldPosition(), 1);
        }
    }
}
