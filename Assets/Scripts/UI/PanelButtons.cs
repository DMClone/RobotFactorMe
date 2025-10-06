using UnityEngine;
using UnityEngine.Events;

public class PanelButtons : MonoBehaviour
{
    [SerializeField] Panel panel;

    public UnityEvent Clicked;

    public void OnClick()
    {
        Clicked.Invoke();
    }

    public void ShowInventory()
    {
        panel.ShowInventory();
    }

    public void ShowGrid()
    {
        panel.ShowGrid();
    }

    public void ShowMap()
    {
        panel.ShowMap();
    }
}
