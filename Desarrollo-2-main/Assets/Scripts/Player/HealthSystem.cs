using UnityEngine;
using UnityEngine.AI;

public class HealthSystem : MonoBehaviour
{
    public float health;
    private LevelController levelController;

    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    protected void DestroyPlayer()
    {
        Destroy(gameObject, 0.5f);
        levelController.EnemyDestroyed(gameObject);
    }
}
