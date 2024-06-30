using Unity.VisualScripting;
using UnityEngine;

public class ProjectileBehaviour : Projectile
{
    protected override void OnUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * projectileData.speed * Time.deltaTime;
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            target.GetComponent<HealthSystem>().TakeDamage((int)projectileData.damage);
            DestroyProjectile();
        }
    }
}
