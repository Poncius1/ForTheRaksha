using UnityEngine;

public class CursorState : MonoBehaviour
{
    public bool cursorLocked = true;

    private void Start()
    {
        SetCursorState(cursorLocked);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !newState; // Ocultar o mostrar el cursor según el estado
    }
}
