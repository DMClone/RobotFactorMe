using UnityEngine;
using UnityEngine.Animations;

public class HorizontalScroll : MonoBehaviour
{
    public GameManager gameManager;
    public Transform content;
    public Camera cam;

    [Header("Scroll Settings")]
    public float scrollSpeed = 1f;
    public float inertiaDamping = 5f;
    public float minX = -50f;  // left clamp
    public float maxX = 50f;   // right clamp


    private Vector3 lastPointerPos;
    private float velocity;
    private bool dragging;

    private float scrollPos;
    public bool pressed;

    void Update()
    {
        Vector2 mouseScreenPos = gameManager.controls.Player.Position.ReadValue<Vector2>();
        Vector3 screenPos3D = new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(cam.transform.position.z));
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos3D);

        if (pressed)
        {
            if (!dragging)
            {
                dragging = true;
                lastPointerPos = worldPos;
                velocity = 0f;
            }
            else
            {
                float delta = worldPos.x - lastPointerPos.x;
                float newX = content.localPosition.x + delta * scrollSpeed;

                content.localPosition = new Vector3(
                    Mathf.Clamp(newX, minX, maxX),
                    content.localPosition.y,
                    content.localPosition.z
                );

                velocity = delta / Time.deltaTime;
                lastPointerPos = worldPos;
            }
        }
        else
        {
            if (dragging)
                dragging = false;

            if (Mathf.Abs(velocity) > 0.01f)
            {
                float newX = content.localPosition.x + velocity * Time.deltaTime;
                newX = Mathf.Clamp(newX, minX, maxX);

                content.localPosition = new Vector3(
                    newX,
                    content.localPosition.y,
                    content.localPosition.z
                );

                // Stop velocity if we hit clamp edges
                if (newX <= minX || newX >= maxX)
                    velocity = 0f;
                else
                    velocity = Mathf.Lerp(velocity, 0, inertiaDamping * Time.deltaTime);
            }
        }
    }
}
