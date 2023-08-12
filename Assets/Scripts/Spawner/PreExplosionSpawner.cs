using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PreExplosionSpawner : Spawner
{
    [Header("Pre-Explosion Spawner")]

    private const string PRE_EXPLOSION_BOX = "PreExplosionBox";
    private static PreExplosionSpawner instance;

    [SerializeField] protected LayerMask explosionLayMask;

    public static PreExplosionSpawner Instance { get => instance; }

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.LogWarning("Only 1 ExplosionSpawner allow to exist");
        }

        instance = this;
    }

    protected override void LoadComponent()
    {
        base.LoadComponent();

        explosionLayMask = LayerMask.GetMask("Stage");
    }

    public virtual void SpawnPreExplodeBox(int length, Vector2 position, Quaternion rotation)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Debug.Log(position);

        Spawn(PRE_EXPLOSION_BOX, position, rotation);
        ExplodeDirection(length, position, rotation, Vector2.up);
        ExplodeDirection(length, position, rotation, Vector2.right);
        ExplodeDirection(length, position, rotation, Vector2.down);
        ExplodeDirection(length, position, rotation, Vector2.left);

        NavMeshManager.Instance.BakeNav();
    }
    protected virtual void ExplodeDirection(int length, Vector2 position, Quaternion rotation, Vector2 direction)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayMask))
        {
            return;
        }

        Spawn(PRE_EXPLOSION_BOX, position, rotation);

        ExplodeDirection(length - 1, position, rotation, direction);
    }
    
}
