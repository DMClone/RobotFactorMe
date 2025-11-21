using System.Collections.Generic;
using UnityEngine;

public class GridInventory : MonoBehaviour
{
    public VerticalScroll verticalScroll;
    public GameObject tileOrigin;
    public List<GameObject> items;

    public int tilesPerRow;
    public float sizePerPixel;
    public int tilePixelSize;
    public int pixelsPadding;

    void Start()
    {
        OrderList();
        verticalScroll.SetContentSize(items.Count, tilesPerRow);
    }

    public void AddItem(GameObject item)
    {
        items.Add(item);
        verticalScroll.SetContentSize(items.Count, tilesPerRow);
    }

    public void RemoveItem(GameObject item)
    {
        items.Remove(item);
    }

    public void OrderList()
    {
        for (int i = 0; i < items.Count; i++)
        {
            int row = i / tilesPerRow;
            int column = i % tilesPerRow;
            Vector3 newPosition = new Vector3(
                (column * (tilePixelSize + pixelsPadding)) * sizePerPixel,
                -(row * (tilePixelSize + pixelsPadding)) * sizePerPixel,
                0f);

            items[i].transform.position = newPosition;
            items[i].transform.SetParent(tileOrigin.transform);
        }
    }
}
