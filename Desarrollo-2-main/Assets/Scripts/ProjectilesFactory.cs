using UnityEngine;

public class ProjectilesFactory
{
    private ProjectilesSO projectileSO;

    /// <summary>
    /// Initializes a new instance of the ProjectilesFactory class with the specified data
    /// </summary>
    public ProjectilesFactory(ProjectilesSO data)
    {
        projectileSO = data;
    }

    /// <summary>
    /// Creates a new projectile at the specified position and rotation
    /// </summary>
    public Projectile CreateProjectile(Vector3 position, Quaternion rotation)
    {
        Projectile projectile = GameObject.Instantiate(projectileSO.projectilePrefab, position, rotation);
        projectile.SetData(projectileSO.data);
        return projectile;
    }
}
