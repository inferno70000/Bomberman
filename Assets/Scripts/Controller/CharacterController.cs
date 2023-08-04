using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : AbstractMonoBehaviour
{
    [Header("Character Controller")]

    [SerializeField] protected Movement movement;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected int bombAmount = 1;
    [SerializeField] protected int bombRadius = 1;
    [SerializeField] protected bool isDead = false;

    public int BombAmount { get => bombAmount; }
    public int BombRadius { get => bombRadius; }
    public float Speed { get => speed; }
    public bool IsDead { get => isDead; }

    protected override void LoadComponent()
    {
        if (movement == null)
        {
            movement = GetComponent<Movement>();
        }
    }

    protected virtual void PlaceBomb()
    {
        if (InputManager.Instance.GetBombKey())
        {
            Transform newBomb = BombSpawner.Instance.Spawn(transform.position, transform.rotation);

            newBomb?.GetComponent<BombController>()?.SetLength(bombRadius);
        }
    }

    public void SetBombAmount(int bombAmount)
    {
        this.bombAmount = bombAmount;
        BombSpawner.Instance.SetBombLimit(bombAmount);
    }

    public void SetBombRadius(int bombRadius)
    {
        this.bombRadius = bombRadius;
    }

    public abstract void SetSpeed(float speed);

    public virtual void SetIsDead(bool isDead)
    {
        this.isDead = isDead;
    }
}
