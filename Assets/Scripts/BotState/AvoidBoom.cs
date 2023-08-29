using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvoidBoom : MonoBehaviour
{
    private bool isAvoid = false;
    private Collider2D boxCollider;

    float avoidDistance = 5f;

    public bool IsAvoid { get => isAvoid; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boom"))
        {
            Debug.Log("collision");

            //botController.SetIsAvoidBoom(true);
            isAvoid = true;

            boxCollider = collision.GetComponent<Collider2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boom"))
        {
            //botController.SetIsAvoidBoom(false);
            isAvoid = false;

        }
    }

    public void MoveAgentOutOfCollider(NavMeshAgent agent)
    {
        // Find a new position outside the collider
        Vector3 newPosition = FindNewPositionOutsideCollider();

        // Move the agent to the new position
        agent.SetDestination(newPosition);
    }

    private Vector3 FindNewPositionOutsideCollider()
    {
        Vector3 newPosition = Vector3.zero;

        Vector3 randomPoint = Random.insideUnitSphere * avoidDistance;
        randomPoint += boxCollider.bounds.center;

        if (!boxCollider.bounds.Contains(randomPoint))
        {
            newPosition = randomPoint;
        }


        return newPosition;
    }
}
