using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    private bool isPauseActive = false;

    private PlayerMovement player;

    private void Awake()
    {
        player = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        player.input.currentActionMap.FindAction("PauseMenu").started += PauseAction_started;
    }

    private void PauseAction_started(InputAction.CallbackContext obj)
    {
        TogglePause();
    }

    void TogglePause()
    {
        isPauseActive = !isPauseActive;

        UnityEngine.Cursor.visible = isPauseActive;
        UnityEngine.Cursor.lockState = isPauseActive ? CursorLockMode.None : CursorLockMode.Locked;

        if (isPauseActive) 
        {
            pausePanel.SetActive(!pausePanel.activeSelf);
        }     
    }

    private void OnDisable()
    {
        player.input.currentActionMap.FindAction("PauseMenu").started -= PauseAction_started;
    }

   
}