using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public string thisLevelName;
    public string nextLevelName;

    private List<GameObject> enemies;

    /// <summary>
    /// Initializes the LevelController and finds all enemies in the scene
    /// </summary>
    private void Awake()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (var enemy in enemies)
        {
            if (enemy != null && enemy.TryGetComponent(out HealthSystem health))
            {
                health.OnDeath += HandleEnemyDeath;
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var enemy in enemies)
        {
            if (enemy == null)
                continue;
            if (enemy.TryGetComponent(out HealthSystem health))
            {
                health.OnDeath -= HandleEnemyDeath;
            }
        }
    }
   
    /// <summary>
    /// Removes the destroyed enemy from the enemies list and checks if all enemies are destroyed
    /// </summary>
    public void HandleEnemyDeath(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            AdvanceLevelIfNoEnemiesPersist();
        }
    }

    /// <summary>
    /// Checks if all enemies are destroyed and advances to the next level if true
    /// </summary>
    private void AdvanceLevelIfNoEnemiesPersist()
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
        NavigationManager.Instance.UnloadScene(thisLevelName);
        NavigationManager.Instance.LoadScene(nextLevelName);
    }
}