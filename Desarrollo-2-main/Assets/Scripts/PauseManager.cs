using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] private InputActionAsset playerActionMap;
    [SerializeField] private InputActionAsset uiActionMap;
    [SerializeField] private string pauseActionName = "PauseMenu";
    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private GameObject menuButton;
    private bool isPauseActive = false;

    /// <summary>
    /// Subscribes to the PauseMenu action when the object is enabled
    /// </summary>
    private void OnEnable()
    {
        var pauseAction = playerActionMap.FindAction(pauseActionName);
        if(pauseAction == null)
            Debug.Log($"{nameof(pauseAction)} is null!");
        else
            pauseAction.started += PauseAction_started;

        var pauseUIAction = uiActionMap.FindAction(pauseActionName);
        if (pauseUIAction == null)
            Debug.Log($"{nameof(pauseUIAction)} is null!");
        else
            pauseUIAction.started += PauseAction_started;

        uiActionMap.Disable();
    }

    /// <summary>
    /// Unsubscribes from the PauseMenu action when the object is disabled
    /// </summary>
    private void OnDisable()
    {
        playerActionMap.FindAction(pauseActionName).started -= PauseAction_started;
        uiActionMap.FindAction(pauseActionName).started -= PauseAction_started;
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

        if (isPauseActive)
        {
            uiActionMap.Enable();
            eventSystem.SetSelectedGameObject(menuButton);
        }       
        else 
            uiActionMap.Disable();

        Cursor.visible = isPauseActive;
        Cursor.lockState = isPauseActive ? CursorLockMode.None : CursorLockMode.Locked;

        pausePanel.SetActive(isPauseActive);

        if (isPauseActive)
            playerActionMap.Disable();
        else
            playerActionMap.Enable();
    }
}