using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Create ProjectilesSO" , fileName = "ProjectilesSO", order = 0)]

public class ProjectilesSO : ScriptableObject
{
    public ProjectileBehaviour projectilePrefab;

    public ProjectileData data = new ProjectileData();
}

[Serializable]
public class ProjectileData 
{
    public float speed;
    public float damage;
    public bool isTargetTracking;
}