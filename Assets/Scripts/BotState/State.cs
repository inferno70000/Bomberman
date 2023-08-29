using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    [Header("State")]

    protected AvoidBoom avoidBoom;

    public enum STATE
    {
        IDLE, CHASE, AVOID, FARM
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE _name;
    protected EVENT stage;
    protected GameObject npc;
    protected Animator animator;
    protected Transform player;
    protected State nextState;
    protected NavMeshAgent agent;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
    {
        npc = _npc;
        agent = _agent;
        animator = _animator;
        player = _player;
    
        avoidBoom = npc.GetComponent<AvoidBoom>();
    }

    public virtual void Enter() { stage = EVENT.UPDATE; }
    public virtual void Update() { stage = EVENT.UPDATE; }
    public virtual void Exit() { stage = EVENT.EXIT; }

    public State Process()
    {
        if (stage == EVENT.ENTER) { Enter(); }
        if (stage == EVENT.UPDATE) { Update(); }
        if (stage == EVENT.EXIT)
        {
            Exit();
            return nextState;
        }

        return this;
    }
}

public class Idle : State
{
    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
        : base(_npc, _agent, _animator, _player)
    {
        _name = STATE.IDLE;
    }

    public override void Enter()
    {
        Debug.Log("Idle");
        base.Enter();
    }

    public override void Update()
    {
        if (avoidBoom.IsAvoid)
        {
            nextState = new Avoid(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
        else if (Vector2.Distance(npc.transform.position, player.transform.position) < 5f)
        {
            nextState = new Chase(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
        else if (StageManager.Instance.TileLocations.Count > 0)
        {
            nextState = new Farm(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        base.Exit();
    }

}

public class Chase : State
{
    public Chase(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
        : base(_npc, _agent, _animator, _player)
    {
        _name = STATE.CHASE;
    }

    public override void Enter()
    {
        
        Debug.Log("Chase");
        base.Enter();
    }

    public override void Update()
    {
        //agent.SetDestination(player.transform.position);
        NavMeshPath path = new();
        agent.CalculatePath(player.transform.position, path);

        Debug.Log(path.status);

        if (path.status == NavMeshPathStatus.PathComplete)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            nextState = new Idle(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }

        if (avoidBoom.IsAvoid)
        {
            nextState = new Avoid(npc, agent, animator, player);
            stage = EVENT.EXIT;
        } 
        
    }

    public override void Exit()
    {

        base.Exit();
    }

}

public class Avoid : State
{

    public Avoid(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
        : base(_npc, _agent, _animator, _player)
    {
        _name = STATE.AVOID;
    }

    public override void Enter()
    {
        Debug.Log("Avoid");
        avoidBoom.MoveAgentOutOfCollider(agent);

        base.Enter();
    }

    public override void Update()
    {
        if (!avoidBoom.IsAvoid)
        {
            nextState = new Idle(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {

        base.Exit();
    }

}

public class Farm : State
{

    public Farm(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player)
        : base(_npc, _agent, _animator, _player)
    {
        _name = STATE.FARM;
    }

    public override void Enter()
    {
        Debug.Log("Farm");

        agent.SetDestination(StageManager.Instance.FindClosestTile(npc.transform.position));

        Debug.Log(StageManager.Instance.FindClosestTile(npc.transform.position));

        base.Enter();
    }

    public override void Update()
    {
        if (Vector3.Distance(npc.transform.position, agent.destination) <= 0.1)
        {
            npc.GetComponent<CharacterController>().PlaceBomb();
        }

        if (avoidBoom.IsAvoid)
        {
            nextState = new Avoid(npc, agent, animator, player);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {

        base.Exit();
    }

}