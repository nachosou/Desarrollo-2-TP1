using UnityEngine;

public class MainMenu : MonoBehaviour
{
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
