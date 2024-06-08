using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;

    public LayerMask whatIsGround;

    public float health;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    float timer;
    bool canReachWalkPoint;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Patroling();
    }

    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
            timer = 0;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            timer += Time.deltaTime;
        }

        Vector3 distanceToWlakPoint = transform.position - walkPoint;

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

        if (distanceToWlakPoint.magnitude < 1f && canReachWalkPoint)
        {
            walkPointSet = false;
        }
    }

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

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            DestroyEnemy();
        }
    }

    protected void DestroyEnemy()
    {
        Destroy(gameObject, 0.5f);
    }
}