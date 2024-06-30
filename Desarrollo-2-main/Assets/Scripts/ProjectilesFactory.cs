using UnityEngine;

public class ProjectilesFactory
{
    private ProjectilesSO projectileSO;

    public ProjectilesFactory(ProjectilesSO data)
    {
        projectileSO = data;
    }

    /// <summary>
    /// Creates a projectile at the specified position and rotation using the data from ProjectilesSO
    /// </summary>
    public Projectile CreateProjectile(Vector3 position, Quaternion rotation)
    {
        Projectile projectile = GameObject.Instantiate(projectileSO.projectilePrefab, position, rotation);
        projectile.SetData(projectileSO.data);
        return projectile;
    }
}
