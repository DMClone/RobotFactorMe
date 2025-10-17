using UnityEngine;

[CreateAssetMenu(menuName = "Grid/GridDataSO")]
public class GridDataSO : ScriptableObject
{
    public int width;
    public int height;
    [SerializeField]
    public int[] tiles;
}
