using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask whatIsGround;

    private Vector3 walkPoint;
    private bool walkPointSet;
    public float walkPointRange;
    private float timer;
    private bool canReachWalkPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patroling();
    }

    /// <summary>
    /// Controls the patrolling behavior of the enemy
    /// </summary>
    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            timer = 0;
        }

        if (walkPointSet)
        {
            if (agent != null)
            {
                agent.SetDestination(walkPoint);
                timer += Time.deltaTime;
            }
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (timer >= 3)
        {
            canReachWalkPoint = false;
            walkPointSet = false;
        }

        if (!canReachWalkPoint)
        {
            SearchWalkPoint();
            canReachWalkPoint = true;
            walkPointSet = true;
        }

        if (distanceToWalkPoint.magnitude < 1f && canReachWalkPoint)
        {
            walkPointSet = false;
        }
    }

    /// <summary>
    /// Searches for a new random walk point within a specified range
    /// </summary>
    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
}
