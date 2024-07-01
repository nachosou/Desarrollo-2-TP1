using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string thisLevelName;

    /// <summary>
    /// The name of the next level to load
    /// </summary>
    [Tooltip("The name of the next level to load.")]
    public string nextLevelName;

    private List<GameObject> enemies;

    /// <summary>
    /// Initializes the LevelController and finds all enemies in the scene
    /// </summary>
    private void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    /// <summary>
    /// Removes the destroyed enemy from the enemies list and checks if all enemies are destroyed
    /// </summary>
    public void EnemyDestroyed(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            CheckEnemies();
        }
    }

    /// <summary>
    /// Checks if all enemies are destroyed and advances to the next level if true
    /// </summary>
    private void CheckEnemies()
    {
        if (enemies.Count == 0)
        {
            AdvanceToNextLevel();
        }
    }

    /// <summary>
    /// Advances to the next level by unloading the current level and loading the next level
    /// </summary>
    public void AdvanceToNextLevel()
    {
        // Find and deactivate all ThirdPersonCamera objects
        var aux = FindObjectsOfType<ThirdPersonCamera>();
        foreach (var obj in aux)
        {
            obj.gameObject.SetActive(false);
        }

        NavigationManager.Instance.UnloadScene(thisLevelName);
        NavigationManager.Instance.LoadScene(nextLevelName);
    }
}