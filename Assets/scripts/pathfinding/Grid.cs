using System.Collections;
using UnityEngine;

public class Grid : MonoBehaviour
{  
    public LayerMask unwalkable;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid; 

    private float nodeDiameter;
    int gridSizeX, gridSizeY;

    void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector2 worldBottomLeft = (Vector2) transform.position + Vector2.left * gridWorldSize.x / 2 + Vector2.down * gridWorldSize.y / 2; 
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = worldBottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + 
                                    Vector2.up * (y * nodeDiameter + nodeRadius);

                bool walkable = Physics2D.OverlapCircle(worldPoint, 0.5f, Globals.instance.blockingLayer) == null;

                grid[x, y] = new Node(walkable, worldPoint);
            }
        }
    }
}
