using UnityEngine;
using UnityEngine.Tilemaps;

public class ExplosionSpawner : Spawner
{
    [Header("Explosion Spawner")]

    private const string EXPLOSION_START = "ExplosionStart";
    private const string EXPLOSION_END = "ExplosionEnd";
    private const string EXPLOSION_MIDDLE = "ExplosionMiddle";
    private static ExplosionSpawner instance;

    [SerializeField] protected LayerMask explosionLayMask;
    [SerializeField] protected Tilemap destructibleTileMap;

    public static ExplosionSpawner Instance { get => instance; }

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

        if (destructibleTileMap ==  null)
        {
            destructibleTileMap = GameObject.Find("Destructibles").GetComponent<Tilemap>();
        }

        explosionLayMask = LayerMask.GetMask("Stage");
    }

    public virtual void Explode(int length, Vector2 position, Quaternion rotation)
    {
        Spawn(EXPLOSION_START, position, rotation);
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
            ClearDestructible(position);

            return;
        }

        Transform newExplosion = Spawn(length > 1 ? EXPLOSION_MIDDLE : EXPLOSION_END, position, rotation);

        newExplosion.GetComponent<ExplosionController>().SetDirection(direction);

        ExplodeDirection(length - 1, position, rotation, direction);
    }

    protected virtual void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTileMap.WorldToCell(position);
        TileBase tile = destructibleTileMap.GetTile(cell);

        if (tile != null)
        {
            BrickSpawner.Instance.Spawn(BrickSpawner.BRICK, position, Quaternion.identity);

            destructibleTileMap.SetTile(cell, null);
        }
    }
}
