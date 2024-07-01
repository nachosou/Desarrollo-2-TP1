using UnityEngine;
using UnityEngine.InputSystem;

public class Cheats : MonoBehaviour
{
    [Tooltip("Reference to the PlayerMovement script.")]
    [SerializeField] private PlayerMovement player;

    [Tooltip("Reference to the LevelController script.")]
    [SerializeField] private LevelController levelController;

    private bool flashModeActive = false;
    private bool godModeActive = false;

    void OnEnable()
    {
        player.input.currentActionMap.FindAction("FlashMode").started += FlashCheat_started;
        player.input.currentActionMap.FindAction("GodMode").started += GodModeCheat_started;
        player.input.currentActionMap.FindAction("NextLevel").performed += NextLevelCheat_performed;
    }

    private void OnDisable()
    {
        player.input.currentActionMap.FindAction("FlashMode").started -= FlashCheat_started;
        player.input.currentActionMap.FindAction("GodMode").started -= GodModeCheat_started;
        player.input.currentActionMap.FindAction("NextLevel").performed -= NextLevelCheat_performed;
    }

    /// <summary>
    /// Toggles the flash mode cheat, adjusting player movement speed
    /// </summary>
    private void FlashCheat_started(InputAction.CallbackContext obj)
    {
        flashModeActive = !flashModeActive;

        if (flashModeActive)
            player.moveSpeed *= 3.0f;
        else
            player.moveSpeed /= 3.0f;
    }

    /// <summary>
    /// Toggles god mode cheat, enabling or disabling invulnerability
    /// </summary>
    private void GodModeCheat_started(InputAction.CallbackContext obj)
    {
        godModeActive = !godModeActive;

        player.GetComponent<PlayerHealthSystem>().isGodModeActive = godModeActive;
    }

    /// <summary>
    /// Activates the cheat to advance to the next level
    /// </summary>
    private void NextLevelCheat_performed(InputAction.CallbackContext obj)
    {
        levelController.AdvanceToNextLevel();
    }
}
