using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WorldButtonHandler : MonoBehaviour
{
    public static WorldButtonHandler instance;

    [Header("References")]
    public Camera cam;
    private GameActions controls;
    [SerializeField] private TileDragHandler tileDragHandler;

    [Header("Settings")]
    public Vector3 mouseWorldPos;
    public UnityEvent MouseReleaseEvent;
    private bool dragBuffer;

    private void Awake()
    {
        instance = this;

        controls = new GameActions();
        controls.Player.Click.performed += ctx => OnClick();
        controls.Player.Click.canceled += ctx => OnClickRelease();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    void OnClick()
    {
        Vector3 worldPos = GetWorldPosition();

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);


        if (hit.collider != null)
        {
            GameObject go = hit.transform.gameObject;
            switch (hit.transform.tag)
            {
                case "Clickable":
                    break;
                case "Tile":
                    if (tileDragHandler.isDragging)
                        tileDragHandler.SwapTile(hit.transform.GetComponent<BaseTile>());
                    else
                    {
                        tileDragHandler.TryPickUpTile(go.GetComponent<BaseTile>(), go.GetComponent<SpriteRenderer>().sprite);
                        dragBuffer = true;
                    }
                    break;
                case "GridDraggable":
                    break;
                default:
                    break;
            }

        }

        if (tileDragHandler.isDragging && !dragBuffer)
            tileDragHandler.DropTile();
        dragBuffer = false;
    }

    void Update()
    {
        mouseWorldPos = GetWorldPosition();

        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.transform.CompareTag("Tile"))
            {
                hit.transform.GetComponent<BaseTile>().OnHover();
            }
            else
            {
                DescriptionText.instance.SetDescription("", -1);
            }
        }
        else
        {
            DescriptionText.instance.SetDescription("", -1);
        }
    }

    Vector3 GetWorldPosition()
    {
        Vector2 screenPos = controls.Player.Position.ReadValue<Vector2>();
        Vector3 screenPos3D = new Vector3(screenPos.x, screenPos.y, Mathf.Abs(cam.transform.position.z));
        return cam.ScreenToWorldPoint(screenPos3D);
    }

    void OnClickRelease()
    {
        MouseReleaseEvent.Invoke();
    }
}
