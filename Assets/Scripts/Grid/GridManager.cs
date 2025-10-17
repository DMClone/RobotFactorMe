using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [Header("References")]
    public GameObject[] tilePrefabs;
    public GridDataSO gridData;
    public GameObject content;
    public HorizontalScroll horizontalScroll;

    [Header("Grid Settings")]
    public float spaceBetweenTiles = 1.0f;

    [Header("Other")]
    public bool createEmptyGrid = false;
    public BaseTile[,] tiles;

    void Start()
    {
        if (gridData.tiles.Length != gridData.width * gridData.height)
            Debug.LogError("GridDataSO tiles array size does not match width*height");
        if (createEmptyGrid)
            gridData.tiles = new int[gridData.width * gridData.height];
        GenerateGrid();
        AssignNeighbors();
        horizontalScroll.minX = 2.25f + -gridData.width * spaceBetweenTiles;
    }

    void GenerateGrid()
    {
        tiles = new BaseTile[gridData.width, gridData.height];
        for (int y = 0; y < gridData.height; y++)
        {
            for (int x = 0; x < gridData.width; x++)
            {
                GameObject obj = Instantiate(tilePrefabs[gridData.tiles[y * gridData.width + x]],
                new Vector3(content.transform.position.x + spaceBetweenTiles * 0.5f + x * spaceBetweenTiles,
                content.transform.position.y - spaceBetweenTiles + y * -spaceBetweenTiles,
                content.transform.localPosition.x - 3),
                Quaternion.identity,
                content.transform);

                BaseTile tile = obj.GetComponent<BaseTile>();
                tile.gridManager = this;
                tile.Initialize(new Vector2Int(x, y));
                tiles[x, y] = tile;
            }
        }
    }



    void AssignNeighbors()
    {
        for (int x = 0; x < gridData.width; x++)
        {
            for (int y = 0; y < gridData.height; y++)
            {
                BaseTile current = tiles[x, y];
                current.neighbors.Clear();

                // 4-directional adjacency
                Vector2Int[] directions = {
                    new Vector2Int(0, 1),
                    new Vector2Int(0, -1),
                    new Vector2Int(1, 0),
                    new Vector2Int(-1, 0)
                };

                foreach (var dir in directions)
                {
                    int nx = x + dir.x;
                    int ny = y + dir.y;
                    if (nx >= 0 && nx < gridData.width && ny >= 0 && ny < gridData.height)
                    {
                        current.neighbors.Add(tiles[nx, ny]);
                    }
                }
            }
        }
    }

    public void AssignNeighborsToSingularTile(BaseTile tile, bool ripple)
    {
        tile.neighbors.Clear();

        Vector2Int[] directions = {
                    new Vector2Int(0, 1),
                    new Vector2Int(0, -1),
                    new Vector2Int(1, 0),
                    new Vector2Int(-1, 0)
                };

        foreach (var dir in directions)
        {
            int nx = tile.gridPosition.x + dir.x;
            int ny = tile.gridPosition.y + dir.y;
            if (nx >= 0 && nx < gridData.width && ny >= 0 && ny < gridData.height)
            {
                if (ripple)
                    tiles[nx, ny].AssignNewNeighbors();
                tile.neighbors.Add(tiles[nx, ny]);
            }
        }
    }

    // private void OnApplicationQuit()
    // {
    //     gridData.tiles = new int[gridData.width * gridData.height];
    //     for (int y = 0; y < gridData.height; y++)
    //     {
    //         for (int x = 0; x < gridData.width; x++)
    //         {
    //             gridData.tiles[y * gridData.height + x] = tiles[x, y].tileID;
    //         }
    //     }
    // }

    // private int GetTileScript()
    // {
    //     for
    // }
}
