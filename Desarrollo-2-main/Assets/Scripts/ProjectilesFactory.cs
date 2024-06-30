using UnityEngine;

public class ProjectilesFactory 
{
    private ProjectilesSO projectileData;

    public ProjectilesFactory(ProjectilesSO data)
    {
        projectileData = data;
    }

    public Projectile CreateProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject projectileObject = GameObject.Instantiate(projectileData.projectilePrefab, position, rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.speed = projectileData.speed;
        return projectile;
    }
}
