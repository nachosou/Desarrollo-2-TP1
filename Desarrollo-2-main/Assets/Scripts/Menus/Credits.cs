using UnityEngine;

public class Credits : MonoBehaviour
{
    public void GoBackToMenu()
    {
        NavigationManager.Instance.UnloadScene("Credits");
        NavigationManager.Instance.LoadScene("Menu");
    }
}
