using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isPauseActive = false;

    private PlayerMovement player;

    /// <summary>
    /// Initializes the PauseManager and finds the PlayerMovement component
    /// </summary>
    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }

    /// <summary>
    /// Subscribes to the PauseMenu action when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        player.input.currentActionMap.FindAction("PauseMenu").started += PauseAction_started;
    }

    /// <summary>
    /// Unsubscribes from the PauseMenu action when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        player.input.currentActionMap.FindAction("PauseMenu").started -= PauseAction_started;
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