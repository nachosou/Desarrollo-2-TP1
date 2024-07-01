using UnityEngine;

public class Credits : MonoBehaviour
{
    /// <summary>
    /// Unloads the Credits scene and loads the Menu scene
    /// </summary>
    public void GoBackToMenu()
    {
        NavigationManager.Instance.UnloadScene("Credits");
        NavigationManager.Instance.LoadScene("Menu");
    }
}
