using UnityEngine;
using System.Collections.Generic;

public class BaseTile : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;

    [Header("Grid Info")]
    public Vector2Int gridPosition;
    public List<BaseTile> neighbors = new List<BaseTile>();

    [Header("Custom Components")]
    public bool isWalkable = true; // replace
    public int heightLevel = 0; // replace

    // Optional: Called after grid is built
    public void Initialize(Vector2Int pos)
    {
        gridPosition = pos;
        name = $"Tile_{pos.x}_{pos.y}";
    }

    protected void CheckNeighbors()
    {
        foreach (var neighbor in neighbors)
        {
            if (!neighbor.isWalkable)
                Debug.Log($"{neighbor.name} is blocked!");
        }
    }

    public void AssignNewNeighbors()
    {
        gridManager.AssignNeighborsToSingularTile(this, false);
    }

}
