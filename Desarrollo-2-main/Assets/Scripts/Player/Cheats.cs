using UnityEngine;
using UnityEngine.InputSystem;

public class Cheats : MonoBehaviour
{
    [SerializeField] PlayerMovement player;
    [SerializeField] LevelController levelController;

    private bool flashModeActive = false;
    private bool godModeActive = false;

    void OnEnable()
    {
        player.input.currentActionMap.FindAction("FlashMode").started += FlashCheat_started;
        player.input.currentActionMap.FindAction("GodMode").started += GodModeCheat_started;
        player.input.currentActionMap.FindAction("NextLevel").performed += NextLevelCheat_performed;
    }

    private void FlashCheat_started(InputAction.CallbackContext obj)
    {
        flashModeActive = !flashModeActive;

        if (flashModeActive)
            player.moveSpeed *= 3.0f;
        else
            player.moveSpeed /= 3.0f;
    }

    private void GodModeCheat_started(InputAction.CallbackContext obj)
    {
        godModeActive = !godModeActive;

        if (godModeActive)
            player.GetComponent<PlayerHealthSystem>().isGodModeActive = true;
        else
            player.GetComponent<PlayerHealthSystem>().isGodModeActive = false;
    }

    private void NextLevelCheat_performed(InputAction.CallbackContext obj)
    {
        levelController.AdvanceToNextLevel();
    }

    private void OnDisable()
    {
        player.input.currentActionMap.FindAction("FlashMode").started -= FlashCheat_started;
        player.input.currentActionMap.FindAction("GodMode").started -= GodModeCheat_started;
        player.input.currentActionMap.FindAction("NextLevel").performed -= NextLevelCheat_performed;
    }
}
