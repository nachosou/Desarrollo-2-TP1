using System.Collections;
using UnityEngine;

public class FinalBoss : Enemy
{
    public Transform firePoint;
    public float teleportCooldown;
    public float teleportRange;
    public float shootingRange;
    public float shootingCooldown;
    public float projectileSpeed;

    private Vector3 initialPosition;
    private Transform player;
    private bool canTeleport = true;
    private bool canShoot = true;

    public ProjectilesSO projectileData;
    private ProjectilesFactory projectileFactory;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        StartCoroutine(TeleportRoutine());
        StartCoroutine(ShootingCooldownRoutine());
        projectileFactory = new ProjectilesFactory(projectileData);
    }

    private void Update()
    {

    }

    private IEnumerator TeleportRoutine()
    {
        while (canTeleport)
        {
            yield return new WaitForSeconds(teleportCooldown);
            TeleportRandomly();
        }
    }

    private void TeleportRandomly()
    {
        if (canTeleport)
        {
            Vector3 randomPosition = initialPosition + new Vector3(Random.Range(-teleportRange, teleportRange), 0f, Random.Range(-teleportRange, teleportRange));
            transform.position = randomPosition;
        }
    }

    private IEnumerator ShootingCooldownRoutine()
    {
        while(canShoot)
        {
            yield return new WaitForSeconds(shootingCooldown);
            ShootProjectile();
        }
    }

    private void ShootProjectile()
    {
        if(canShoot) 
        {
            Vector3 position = transform.position + transform.forward;
            Quaternion rotation = transform.rotation;
            Projectile projectile = projectileFactory.CreateProjectile(position, rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = (player.position - firePoint.position).normalized * projectileSpeed;
            }
        }       
    }
}
