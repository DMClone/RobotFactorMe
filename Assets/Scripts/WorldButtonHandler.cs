using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WorldButtonHandler : MonoBehaviour
{
    public static WorldButtonHandler instance;

    public Camera cam;
    private GameActions controls;
    public UnityEvent MouseReleaseEvent;

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

            if (hit.transform.CompareTag("Clickable"))
            {
                hit.transform.GetComponent<PanelButtons>().OnClick();
            }

            if (hit.transform.CompareTag("Tile"))
            {
                hit.transform.GetComponent<BaseTile>().OnClick();
            }

            if (hit.transform.CompareTag("GridDraggable"))
            {
                hit.transform.GetComponent<DragHandle>().OnClick();
            }
        }
    }

    void Update()
    {
        Vector3 vector3 = GetWorldPosition();

        RaycastHit2D hit = Physics2D.Raycast(vector3, Vector2.zero);
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
