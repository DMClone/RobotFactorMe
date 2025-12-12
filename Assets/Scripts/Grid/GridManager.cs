using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

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

    private void Start()
    {
        instance = this;

        if (gridData.tiles.Length != gridData.width * gridData.height)
            Debug.LogError("GridDataSO tiles array size does not match width*height");
        if (createEmptyGrid)
            EmptyGridData();
        GenerateGrid();
        AssignNeighbors();
        horizontalScroll.minX = 2.25f + -gridData.width * spaceBetweenTiles;
    }

    private void EmptyGridData()
    {
        gridData.tiles = new int[gridData.width * gridData.height];
    }

    private void GenerateGrid()
    {
        tiles = new BaseTile[gridData.width, gridData.height];
        for (int y = 0; y < gridData.height; y++)
        {
            for (int x = 0; x < gridData.width; x++)
            {
                int tileIndex = gridData.tiles[y * gridData.width + x];
                GameObject obj = Instantiate(tilePrefabs[tileIndex],
                new Vector3(content.transform.position.x + spaceBetweenTiles * 0.5f + x * spaceBetweenTiles,
                content.transform.position.y - spaceBetweenTiles + y * -spaceBetweenTiles,
                content.transform.localPosition.x - 3),
                Quaternion.identity,
                content.transform);

                BaseTile tile = obj.GetComponent<BaseTile>();
                tile.isInGrid = true;
                tile.tileID = gridData.tiles[y * gridData.width + x];
                tile.Initialize(new Vector2Int(x, y));
                tiles[x, y] = tile;
            }
        }
    }



    private void AssignNeighbors()
    {
        // 4-directional adjacency
        Vector2Int[] directions = {
        new Vector2Int(0, 1),
        new Vector2Int(0, -1),
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0)
        };

        for (int x = 0; x < gridData.width; x++)
        {
            for (int y = 0; y < gridData.height; y++)
            {
                BaseTile current = tiles[x, y];
                current.neighbors.Clear();


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

    public void PlaceTileAt(int x, int y, int tileID)
    {
        Destroy(tiles[x, y].gameObject);

        GameObject obj = Instantiate(tilePrefabs[tileID],
            new Vector3(content.transform.position.x + spaceBetweenTiles * 0.5f + x * spaceBetweenTiles,
            content.transform.position.y - spaceBetweenTiles + y * -spaceBetweenTiles,
            content.transform.localPosition.x - 3),
            Quaternion.identity,
            content.transform);

        BaseTile tile = obj.GetComponent<BaseTile>();
        tile.tileID = tileID;
        tile.isInGrid = true;
        tile.Initialize(new Vector2Int(x, y));
        tiles[x, y] = tile;

        AssignNeighborsToSingularTile(tile, true);
    }

    private void OnApplicationQuit()
    {

        for (int y = 0; y < gridData.height; y++)
        {
            for (int x = 0; x < gridData.width; x++)
            {
                gridData.tiles[y * gridData.width + x] = tiles[x, y].tileID;
            }
        }
        Debug.Log("Grid data saved on application quit.");
    }

    //Debug
    [ContextMenu("Create Blank Grid")]
    private void CreateBlankGrid()
    {
        EmptyGridData();
        GenerateGrid();
    }

}
