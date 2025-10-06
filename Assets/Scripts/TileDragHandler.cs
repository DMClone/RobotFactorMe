// using UnityEngine;
// using UnityEngine.Tilemaps;
// using UnityEngine.InputSystem;

// public class TileDragHandler : MonoBehaviour
// {
//     public Tilemap gridTilemap;
//     public Tilemap InventoryTilemap;
//     public Color placeholderColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

//     private TileBase draggedTile;
//     private Vector3Int originalPos;
//     private TileBase placeholderTile;
//     private bool isDragging;

//     void Update()
//     {
//         Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
//         Vector3Int cellPos = gridTilemap.WorldToCell(mouseWorldPos);

//         if (Mouse.current.leftButton.wasPressedThisFrame)
//         {
//             TryPickUpTile(cellPos);
//         }

//         if (isDragging)
//         {
//             // Show placeholder on the original position
//             gridTilemap.SetTile(originalPos, placeholderTile);

//             // Optionally, you can create a "ghost" tile following the cursor
//         }

//         if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
//         {
//             DropTile(InventoryTilemap.WorldToCell(mouseWorldPos));
//         }
//     }

//     void TryPickUpTile(Vector3Int pos)
//     {
//         TileBase tile = gridTilemap.GetTile(pos);
//         if (tile != null)
//         {
//             draggedTile = tile;
//             originalPos = pos;

//             // Create placeholder tile
//             placeholderTile = ScriptableObject.CreateInstance<Tile>();
//             placeholderTile = tile.sprite;
//             placeholderTile.color = placeholderColor;

//             isDragging = true;

//             // Remove the tile from source (or leave placeholder)
//             gridTilemap.SetTile(pos, placeholderTile);
//         }
//     }

//     void DropTile(Vector3Int pos)
//     {
//         Tilemap dropMap = InventoryTilemap; // could add logic to pick map based on mouse
//         if (dropMap.GetTile(pos) == null)
//         {
//             dropMap.SetTile(pos, draggedTile);
//         }
//         else
//         {
//             // Optionally swap or cancel
//             gridTilemap.SetTile(originalPos, draggedTile);
//         }

//         draggedTile = null;
//         placeholderTile = null;
//         isDragging = false;
//     }
// }
