using System.Collections.Generic;
using UnityEngine;

public class GridInventory : MonoBehaviour
{
    public VerticalScroll verticalScroll;
    public GameObject tileOrigin;
    public List<GameObject> items;

    public int tilesPerRow;
    public float spaceBetweenTiles;

    void Start()
    {
        OrderList();
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
                            tileOrigin.transform.position.x + spaceBetweenTiles * 0.5f + column * spaceBetweenTiles,
                            tileOrigin.transform.position.y - spaceBetweenTiles + row * -spaceBetweenTiles,
                            tileOrigin.transform.position.z);

            items[i].transform.position = newPosition;
            items[i].transform.SetParent(tileOrigin.transform);
        }
    }
}
