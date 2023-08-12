using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class BotController : CharacterController
{
    [Header("AI Controller")]
    public Transform player;

    Animator animator;
    State currentState;
    NavMeshAgent agent;
    NavMeshPath path;
    float elapsed = 0;
    

    protected override void LoadComponent()
    {
        base.LoadComponent();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponentInChildren<Animator>();
        currentState = new Idle(gameObject, agent, animator, player);

        path = new();
        elapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        currentState = currentState.Process();

        // Update the way to the goal every second.
        //elapsed += Time.deltaTime;
        //if (elapsed > 1.0f)
        //{
            //elapsed -= 1.0f;
            NavMesh.CalculatePath(transform.position,
                player.transform.position, NavMesh.AllAreas, path);
        //}

        for (int i = 0; i < path.corners.Length - 1; i++)
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
    }

    public override void SetSpeed(float speed)
    {
        this.speed = speed;
        agent.speed = speed;
    }
}
