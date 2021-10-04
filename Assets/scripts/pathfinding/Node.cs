using UnityEngine;
public class Node
{
    public bool walkable;
    public Vector2 worldPosition;
    public int gridX, gridY;

    public int gCost, hCost;
    public Node parent;

    public Node(bool walkable, Vector2 worldPosition, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost
    {
        get 
        {
            return gCost + fCost;
        }
    }
}