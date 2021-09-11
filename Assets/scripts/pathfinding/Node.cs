using UnityEngine;
using System.Collections;

public class Node
{
    public bool _walkable;
    public Vector2 _worldPosition;

    public Node(bool walkable, Vector2 worldPosition)
    {
        this._walkable = walkable;
        this._worldPosition = worldPosition;
    }
}