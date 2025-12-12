using UnityEngine;
using TMPro;

public class DescriptionText : MonoBehaviour
{
    public static DescriptionText instance;

    public TextMeshProUGUI text;
    public int showingTileID;

    void Awake()
    {
        instance = this;
    }

    public void SetDescription(string desc, int tileID)
    {
        text.text = desc;
        showingTileID = tileID;
    }
}
