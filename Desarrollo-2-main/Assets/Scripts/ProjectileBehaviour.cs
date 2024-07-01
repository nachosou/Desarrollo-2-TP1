using UnityEngine;

public class ProjectileBehaviour : Projectile
{
    /// <summary>
    /// Updates the projectile's position and rotation to follow the target
    /// </summary>
    protected override void OnUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * projectileData.speed * Time.deltaTime;
        transform.LookAt(target.transform.position);
    }

    /// <summary>
    /// Destroys the projectile after a short delay.
    /// </summary>
    protected void DestroyProjectile()
    {
        Destroy(gameObject, 0.5f);
    }

    /// <summary>
    /// Handles the collision event to apply damage and destroy the projectile
    /// </summary>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            target.GetComponent<PlayerHealthSystem>().TakeDamage((int)projectileData.damage);
            DestroyProjectile();
        }
        else if (collision.collider.gameObject.CompareTag("Enemy"))
        {
            target = collision.collider.transform;
            target.GetComponent<HealthSystem>().TakeDamage((int)projectileData.damage);
            DestroyProjectile();
        }
    }
}
