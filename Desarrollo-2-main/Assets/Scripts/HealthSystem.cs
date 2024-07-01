using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;

    private LevelController levelController;

    /// <summary>
    /// Initializes the health system and finds the LevelController in the scene
    /// </summary>
    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    /// <summary>
    /// Reduces health by the specified damage amount and destroys the entity if health reaches zero
    /// </summary>
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    /// <summary>
    /// Destroys the entity and notifies the LevelController
    /// </summary>
    protected void DestroyPlayer()
    {
        Destroy(gameObject, 0.5f);
        levelController.EnemyDestroyed(gameObject);
    }
}
