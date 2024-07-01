using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Tooltip("Reference to the LevelController script.")]
    [SerializeField] private LevelController levelController;

    /// <summary>
    /// Loads Level 1 scene and unloads the main menu
    /// </summary>
    public void PlayGame()
    {
        NavigationManager.Instance.UnloadScene("Menu");
        NavigationManager.Instance.LoadScene("Level 1");
    }

    /// <summary>
    /// Loads Credits scene and unloads the main menu
    /// </summary>
    public void Credits()
    {
        NavigationManager.Instance.UnloadScene("Menu");
        NavigationManager.Instance.LoadScene("Credits");
    }

    /// <summary>
    /// Returns to the main menu from the current level or scene
    /// </summary>
    public void GoToMenu()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene("Menu");
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
}
