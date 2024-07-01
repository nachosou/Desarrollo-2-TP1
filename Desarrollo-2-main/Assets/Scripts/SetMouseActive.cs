using UnityEngine;

public class SetMouseActive : MonoBehaviour
{
    void Awake()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
}
