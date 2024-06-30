using UnityEngine;

[CreateAssetMenu(menuName = "Create ProjectilesSO" , fileName = "ProjectilesSO", order = 0)]

public class ProjectilesSO : ScriptableObject
{
    public GameObject projectilePrefab;

    public float speed;
}
