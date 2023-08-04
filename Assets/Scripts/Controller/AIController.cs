using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : CharacterController
{
    NavMeshAgent agent;

    public GameObject player;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);
    }

    public override void SetSpeed(float speed)
    {
        this.speed = speed;
        agent.speed = speed;
    }
}
