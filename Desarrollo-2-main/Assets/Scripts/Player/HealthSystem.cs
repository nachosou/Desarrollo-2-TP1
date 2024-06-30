using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float health;

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
    }
}
