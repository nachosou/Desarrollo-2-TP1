using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected ProjectileData projectileData;
    protected Transform target;

    public void SetData(ProjectileData projectileData)
    {
        this.projectileData = projectileData;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual void OnUpdate() { }

    private void Update()
    {
        if (target != null && projectileData != null)
        {
            OnUpdate();
        }
    }
}
