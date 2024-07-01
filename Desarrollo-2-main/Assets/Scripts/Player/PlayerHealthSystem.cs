using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public bool isGodModeActive = false;
    public float health;

    /// <summary>
    /// Reduces the player's health by the specified damage amount if God Mode is not active
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (!isGodModeActive)
        {
            health -= damage;
        }

        if (health <= 0)
        {
            DestroyPlayer();
        }
    }

    /// <summary>
    /// Destroys the player after a short delay
    /// </summary>
    protected void DestroyPlayer()
    {
        Destroy(gameObject, 0.5f);
    }
}
