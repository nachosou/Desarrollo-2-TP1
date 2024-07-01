using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] private InputActionAsset actionMap;
    [SerializeField] private string pauseActionName = "PauseMenu";
    private bool isPauseActive = false;

    /// <summary>
    /// Subscribes to the PauseMenu action when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        var pauseAction = actionMap.FindAction(pauseActionName);
        if(pauseAction == null)
            Debug.Log($"{nameof(pauseAction)} is null!");
        else
            pauseAction.started += PauseAction_started;
    }

    /// <summary>
    /// Unsubscribes from the PauseMenu action when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        actionMap.FindAction(pauseActionName).started -= PauseAction_started;
    }

    /// <summary>
    /// Handles the event when the pause action is started
    /// </summary>
    private void PauseAction_started(InputAction.CallbackContext obj)
    {
        TogglePause();
    }

    /// <summary>
    /// Toggles the pause state and updates the pause panel and cursor visibility
    /// </summary>
    private void TogglePause()
    {
        isPauseActive = !isPauseActive;

        Cursor.visible = isPauseActive;
        Cursor.lockState = isPauseActive ? CursorLockMode.None : CursorLockMode.Locked;

        pausePanel.SetActive(isPauseActive);
    }
}