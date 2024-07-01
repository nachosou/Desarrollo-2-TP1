using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected ProjectileData projectileData;

    /// <summary>
    /// The target the projectile is aimed at.
    /// </summary>
    protected Transform target;

    /// <summary>
    /// Sets the data for the projectile
    /// </summary>
    public void SetData(ProjectileData projectileData)
    {
        this.projectileData = projectileData;
    }

    /// <summary>
    /// Sets the target for the projectile
    /// </summary>
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    /// <summary>
    /// Called every frame to update the projectile's behavior
    /// </summary>
    protected virtual void OnUpdate() { }

    /// <summary>
    /// Updates the projectile each frame if it has a target and data
    /// </summary>
    private void Update()
    {
        if (target != null && projectileData != null)
        {
            OnUpdate();
        }
    }
}
