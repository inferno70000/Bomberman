using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BombSpawner : Spawner
{
    [Header("Bomb Spawner")]

    private static BombSpawner instance;
    //[SerializeField] private int bombLimit = 1;

    public static BombSpawner Instance { get => instance; }
    //protected int BombLimit { get => bombLimit; }

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Only 1 BombSpawner allow to exist");
        }

        instance = this;
    }

    protected override void SetPrefab(Transform prefab, string prefabName, Vector2 position, Quaternion rotation)
    {
        base.SetPrefab(prefab, prefabName, position, rotation);

        BombController bombController = prefab.GetComponent<BombController>();

        if (bombController != null)
        {
            bombController.GetCircleCollider2D().isTrigger = true;
        }
    }

    /// <summary>
    /// spawn a boom if meeting conditions, else return null
    /// </summary>
    public override Transform Spawn(Vector2 position, Quaternion rotation)
    {
        //if (CountBombActive() >= bombLimit)
        //{
        //    return null;
        //}

        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        if (IsBombExisted(position))
        {
            return null;
        }

        return base.Spawn(position, rotation);
    }

    /// <summary>
    /// Check if position existed a bomb
    /// </summary>
    protected virtual bool IsBombExisted(Vector2 position)
    {
        foreach (Transform prefab in holder)
        {
            if (prefab.gameObject.activeSelf && (Vector2)prefab.position == position)
            {
                return true;
            }
        }

        return false;
    }

    //protected virtual int CountBombActive()
    //{
    //    int count = 0;
    //    foreach(Transform prefab in holder)
    //    {
    //        if (prefab.gameObject.activeSelf)
    //        {
    //            count++;
    //        }
    //    }

    //    return count;
    //}

    //public virtual void SetBombLimit(int bombLimit)
    //{
    //    this.bombLimit = bombLimit;
    //}
}
