using UnityEngine;

public class DragHandle : MonoBehaviour
{
    public DragScreen dragScreen;

    public void OnClick()
    {
        dragScreen.pressed = true;
        WorldButtonHandler.instance.MouseReleaseEvent.AddListener(OnRelease);
    }

    void OnRelease()
    {
        dragScreen.pressed = false;
        WorldButtonHandler.instance.MouseReleaseEvent.RemoveListener(OnRelease);
    }
}