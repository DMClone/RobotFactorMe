using UnityEngine;
using DG.Tweening;

public class MainUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private RectTransform uiPanel;
    [Header("Settings")]
    [SerializeField] private int raisedUiHeight = 100;
    [SerializeField] private int loweredUiHeight = -100;
    [SerializeField][ReadOnly] private bool isUIRaised = true;

    public void ToggleUI()
    {
        if (isUIRaised)
        {
            uiPanel.DOAnchorPosY(loweredUiHeight, 0.5f).SetEase(Ease.OutQuad);
            isUIRaised = false;
        }
        else
        {
            uiPanel.DOAnchorPosY(raisedUiHeight, 0.5f).SetEase(Ease.OutQuad);
            isUIRaised = true;
        }
    }
}
