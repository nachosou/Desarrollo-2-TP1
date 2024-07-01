using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;
    public event Action<GameObject> OnDeath;

    /// <summary>
    /// Reduces health by the specified damage amount and destroys the entity if health reaches zero
    /// </summary>
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Destroys the entity and notifies the LevelController
    /// </summary>
    protected void Die()
    {
        Destroy(gameObject, 0.1f);
        OnDeath?.Invoke(gameObject);
    }
}
