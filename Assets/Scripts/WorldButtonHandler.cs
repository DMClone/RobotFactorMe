using UnityEngine;
using UnityEngine.InputSystem;

public class WorldButtonHandler : MonoBehaviour
{
    public Camera cam;
    public HorizontalScroll horizontalScroll;
    private GameActions controls;

    private void Awake()
    {
        controls = new GameActions();
        controls.Player.Click.performed += ctx => OnClick();
        controls.Player.Click.canceled += ctx => OnClickRelease();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    void OnClick()
    {
        Debug.Log("Clicked");
        Vector2 screenPos = controls.Player.Position.ReadValue<Vector2>();
        Vector3 screenPos3D = new Vector3(screenPos.x, screenPos.y, Mathf.Abs(cam.transform.position.z));
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos3D);
        Debug.Log("worldPos = " + worldPos);

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Clicked on: " + hit.collider.name);

            Debug.Log("Their tag is " + hit.transform.tag);

            if (hit.transform.CompareTag("Clickable"))
            {
                hit.transform.GetComponent<PanelButtons>().OnClick();
            }

            if (hit.transform.CompareTag("GridDraggable"))
            {
                horizontalScroll.pressed = true;
            }
        }
    }

    void OnClickRelease()
    {
        Debug.Log("Click Released");
        horizontalScroll.pressed = false;
    }

    void StartGame()
    {
        Debug.Log("Starting game!");
        // Add your logic here
    }
}
