using UnityEngine;

/// <summary>
/// Controls the orientation of a projectile to look at a specified target
/// </summary>
public class ProjectileLookAt : MonoBehaviour
{
    [Tooltip("The target GameObject that the projectile should look at.")]
    [SerializeField] private GameObject target;

    private void Update()
    {
        ProjetileLookAtTarget();
    }

    private void ProjetileLookAtTarget()
    {
        if (target != null)
        {
            transform.LookAt(target.transform.position);
        }
    }
}
