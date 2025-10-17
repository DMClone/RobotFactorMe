using UnityEngine;

public class SpecialTile : BaseTile
{
    void Start()
    {
        for (int i = 0; i < neighbors.Count; i++)
        {
            if (!neighbors[i].isWalkable)
            {
                Debug.Log(neighbors[i].name + " is blocked!");
            }
        }
    }
}
