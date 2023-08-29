using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public abstract class CharacterController : AbstractMonoBehaviour
{
    [Header("Character Controller")]

    [SerializeField] protected Movement movement;
    [SerializeField] protected float speed = 5f;
    [SerializeField] protected int bombAmount = 1;
    [SerializeField] protected int bombRadius = 1;
    [SerializeField] protected bool isDead = false;

    protected List<Transform> boomHolder = new();

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

    public virtual void PlaceBomb()
    {
        if (CountBombActive() < BombAmount)
        {
            Transform newBomb = BombSpawner.Instance.Spawn(transform.position, transform.rotation);

            PreExplosionSpawner.Instance.SpawnPreExplodeBox(bombRadius, transform.position, transform.rotation);

            newBomb?.GetComponent<BombController>()?.SetLength(bombRadius);

            if (boomHolder.Exists(x => x == newBomb) || newBomb == null)
            {
                return;
            }

            boomHolder.Add(newBomb);
        }
    }

    public void SetBombAmount(int bombAmount)
    {
        this.bombAmount = bombAmount;
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

    protected virtual int CountBombActive()
    {
        int count = 0;
        foreach (Transform prefab in boomHolder)
        {
            if (prefab.gameObject.activeSelf)
            {
                count++;
            }
        }

        return count;
    }
}
