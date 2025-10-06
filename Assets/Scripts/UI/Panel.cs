using UnityEngine;

public class Panel : MonoBehaviour
{
    public GameObject inventory;
    public GameObject grid;
    public GameObject map;

    public void ShowInventory()
    {
        if (grid.activeSelf == false)
        {
            inventory.SetActive(true);
            grid.SetActive(false);
            map.SetActive(false);
        }
    }

    public void ShowGrid()
    {
        if (grid.activeSelf == false)
        {
            inventory.SetActive(false);
            grid.SetActive(true);
            map.SetActive(false);
        }
    }

    public void ShowMap()
    {
        if (map.activeSelf == false)
        {
            inventory.SetActive(false);
            grid.SetActive(false);
            map.SetActive(true);
        }
    }
}
