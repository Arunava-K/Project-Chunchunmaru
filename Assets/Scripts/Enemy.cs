using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    private NavMeshAgent agent;
    public float stopFollowingDistance;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (player == null)
        {
            Debug.LogError("Player reference is not set for the enemy!");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            if(Vector3.Distance(transform.position, player.position) <= stopFollowingDistance)
            { 
                agent.isStopped = true;
            }
            else
            {
                agent.isStopped = false;
                // Set the destination of the NavMeshAgent to the player's position
                agent.SetDestination(player.position);
            }
        }
    }
}
