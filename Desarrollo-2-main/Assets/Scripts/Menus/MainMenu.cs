using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] LevelController levelController;

    public void PlayGame()
    {
        NavigationManager.Instance.UnloadScene("Menu");
        NavigationManager.Instance.LoadScene("Level 1");
    }

    public void Credits()
    {
        NavigationManager.Instance.UnloadScene("Menu");
        NavigationManager.Instance.LoadScene("Credits");
    }

    public void GoToMenu()
    {
        NavigationManager.Instance.UnloadScene(levelController.thisLevelName);
        NavigationManager.Instance.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
