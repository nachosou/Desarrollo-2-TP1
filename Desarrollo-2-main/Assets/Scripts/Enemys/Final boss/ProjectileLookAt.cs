using UnityEngine;

public class ProjectileLookAt : MonoBehaviour
{
    [SerializeField] private GameObject target;

    private void Update()
    {
        ProjetileLookAtTarget();
    }

    private void ProjetileLookAtTarget()
    {
        transform.forward = target.transform.position;
    }
}
