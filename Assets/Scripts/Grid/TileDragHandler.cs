using UnityEngine;
using UnityEngine.InputSystem;

public class TileDragHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private WorldButtonHandler worldButtonHandler;
    [SerializeField] private GameObject gridObject;
    [SerializeField] private SpriteRenderer gridObjectSpriteRenderer;

    public Color placeholderColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);

    [Header("Settings")]
    private BaseTile draggingTile;
    private Vector2Int originalGridPos;
    private Vector3 originalPos;
    public bool isDragging;

    void LateUpdate()
    {
        if (isDragging && draggingTile != null)
        {
            Vector3 mouseWorldPos = worldButtonHandler.mouseWorldPos;
            gridObject.transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, gridObject.transform.position.z);
        }
    }

    public void TryPickUpTile(BaseTile baseTile, Sprite tileSprite)
    {
        Debug.Log("Picking up tile");
        draggingTile = baseTile;
        baseTile.GetComponent<SpriteRenderer>().color = placeholderColor;
        gridObject.SetActive(true);
        gridObjectSpriteRenderer.sprite = tileSprite;
        originalGridPos = draggingTile.gridPosition;
        originalPos = draggingTile.transform.position;
        isDragging = true;
    }

    public void SwapTile(BaseTile targetTile)
    {
        if (targetTile.isInGrid)
        {
            Debug.Log("Swapping tiles");
            int tempID = targetTile.tileID;
            Vector2Int targetPos = targetTile.gridPosition;
            draggingTile.GetComponent<SpriteRenderer>().color = Color.white;
            GridManager.instance.PlaceTileAt(targetPos.x, targetPos.y, draggingTile.tileID);
            GridManager.instance.PlaceTileAt(originalGridPos.x, originalGridPos.y, tempID);
            gridObject.SetActive(false);
            Destroy(targetTile.gameObject);
            Destroy(draggingTile.gameObject);
            isDragging = false;
        }
    }

    public void DropTile()
    {
        Debug.Log("Dropping tile");
        gridObject.SetActive(false);
        isDragging = false;
        draggingTile = null;
    }
}
