using UnityEngine;

public class VerticalScroll : DragScreen
{
    public GameManager gameManager;
    public Transform content;
    public Camera cam;

    [Header("Scroll Settings")]
    public float scrollSpeed = 1f;
    public float inertiaDamping = 5f;
    public float startingHeight = 0f;
    public float heightPerRow = 1f;
    public float minY;  // bottom clamp
    public float maxY = 1;   // top clamp

    private Vector3 lastPointerPos;
    private float velocity;
    private bool dragging;

    private float scrollPos;

    void Update()
    {
        Vector2 mouseScreenPos = gameManager.controls.Player.Position.ReadValue<Vector2>();
        Vector3 screenPos3D = new Vector3(mouseScreenPos.x, mouseScreenPos.y, Mathf.Abs(cam.transform.position.z));
        Vector3 worldPos = cam.ScreenToWorldPoint(screenPos3D);

        // Normalize clamp values in case minY > maxY
        float min = Mathf.Min(minY, maxY);
        float max = Mathf.Max(minY, maxY);

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
                float delta = worldPos.y - lastPointerPos.y;
                float newY = content.localPosition.y + delta * scrollSpeed;

                newY = Mathf.Clamp(newY, min, max);
                content.localPosition = new Vector3(content.localPosition.x, newY, content.localPosition.z);

                velocity = delta / Time.deltaTime;
                lastPointerPos = worldPos;
            }
        }
        else
        {
            if (dragging)
                dragging = false;

            // Apply inertia
            if (Mathf.Abs(velocity) > 0.01f)
            {
                float newY = content.localPosition.y + velocity * Time.deltaTime;
                newY = Mathf.Clamp(newY, min, max);
                content.localPosition = new Vector3(content.localPosition.x, newY, content.localPosition.z);

                // Dampen the velocity
                velocity = Mathf.Lerp(velocity, 0f, inertiaDamping * Time.deltaTime);
            }
            else
            {
                velocity = 0f;
            }
        }
    }

    public void SetContentSize(int itemCount, int itemsPerRow)
    {
        int totalRows = Mathf.CeilToInt((float)itemCount / itemsPerRow);
        float contentHeight = startingHeight + (totalRows - 1) * heightPerRow;
        minY = contentHeight - startingHeight;
    }
}