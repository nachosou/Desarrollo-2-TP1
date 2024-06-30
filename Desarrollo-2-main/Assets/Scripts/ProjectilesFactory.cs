using UnityEngine;

public class ProjectilesFactory 
{
    private ProjectilesSO projectileData;

    public ProjectilesFactory(ProjectilesSO data)
    {
        projectileData = data;
    }

    /// <summary>
    /// Creates a projectile at the specified position and rotation using the data from ProjectilesSO
    /// </summary>
    public Projectile CreateProjectile(Vector3 position, Quaternion rotation)
    {
        GameObject projectileObject = GameObject.Instantiate(projectileData.projectilePrefab, position, rotation);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.speed = projectileData.speed;
        return projectile;
    }
}
