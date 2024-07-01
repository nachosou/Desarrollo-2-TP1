using UnityEngine;

public class FinalBoss : Enemy
{
    public float teleportRange;

    private Vector3 initialPosition;
    private Transform player;
    private bool canTeleport = true;
    private bool canShoot = true;

    public ProjectilesSO projectileSO;
    private ProjectilesFactory projectileFactory;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialPosition = transform.position;
        projectileFactory = new ProjectilesFactory(projectileSO);    
    }

    private void Update()
    {
        transform.LookAt(player.transform.position);
    }

    public void TeleportRandomly()
    {
        if (canTeleport)
        {
            Vector3 randomPosition = initialPosition + new Vector3(Random.Range(-teleportRange, teleportRange), 0f, Random.Range(-teleportRange, teleportRange));
            transform.position = randomPosition;
        }
    }

    public void ShootProjectile()
    {
        if (canShoot)
        {
            Vector3 position = transform.position + transform.forward;
            Quaternion rotation = transform.rotation;
            projectileFactory.CreateProjectile(position, rotation).SetTarget(player.transform);
        }
    }
}
