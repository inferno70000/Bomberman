using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotAnimation : CharacterAnimation
{
    [SerializeField] NavMeshAgent agent;

    protected override void LoadComponent()
    {
        base.LoadComponent();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = Vector2.ClampMagnitude(agent.desiredVelocity, 1);
        Vector2 roundedVelocity = new(Mathf.Round(velocity.x), 
                                      Mathf.Round(velocity.y));

        Animate(roundedVelocity);
    }
}
