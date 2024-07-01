using System;
using UnityEngine;

/// <summary>
/// Scriptable object for storing projectile settings and data.
/// </summary>
[CreateAssetMenu(menuName = "Create ProjectilesSO", fileName = "ProjectilesSO", order = 0)]
public class ProjectilesSO : ScriptableObject
{
    public ProjectileBehaviour projectilePrefab;

    public ProjectileData data = new ProjectileData();
}

/// <summary>
/// Class for storing data related to a projectile.
/// </summary>
[Serializable]
public class ProjectileData
{
    public float speed;
    public float damage;
    public bool isTargetTracking;
}