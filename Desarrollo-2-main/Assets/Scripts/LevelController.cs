using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public string thisLevelName; 
    public string nextLevelName; 

    private List<GameObject> enemies;

    private void Start()
    {
        enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
    }

    public void EnemyDestroyed(GameObject enemy)
    {
        if (enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
            CheckEnemies();
        }
    }

    private void CheckEnemies()
    {
        if (enemies.Count == 0)
        {
            AdvanceToNextLevel();
        }
    }

    [ContextMenu("Next Level")]
    private void AdvanceToNextLevel()
    {
        var aux = FindObjectsOfType<ThirdPersonCamera>(); 
        foreach (var obj in aux) 
        { 
            obj.gameObject.SetActive(false);
        }

        NavigationManager.Instance.UnloadScene(thisLevelName);
        NavigationManager.Instance.LoadScene(nextLevelName);
    }
}