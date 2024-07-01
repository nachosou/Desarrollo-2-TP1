using UnityEngine;

public class ProjectileBehaviour : Projectile
{
    protected override void OnUpdate()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * projectileData.speed * Time.deltaTime;
        transform.LookAt(target.transform.position);
    }

    protected void DestroyProjectile()
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == target)
        {
            target.GetComponent<PlayerHealthSystem>().TakeDamage((int)projectileData.damage);
            DestroyProjectile();
        }
        else if(collision.collider.gameObject.CompareTag("Enemy"))
        {
            target = collision.collider.transform;
            target.GetComponent<HealthSystem>().TakeDamage((int)projectileData.damage);
            DestroyProjectile();
        }
    }
}
