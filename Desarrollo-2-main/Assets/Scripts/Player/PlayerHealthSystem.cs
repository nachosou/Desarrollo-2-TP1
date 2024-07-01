using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    public bool isGodModeActive = false;
    public float health;

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

    protected void DestroyPlayer()
    {
        Destroy(gameObject, 0.5f);
    }
}
