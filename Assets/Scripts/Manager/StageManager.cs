using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public sealed class StageManager : AbstractMonoBehaviour
{
    private static StageManager instance;

    Tilemap destructibles;
    [SerializeField] List<Vector3> tileLocations = new();

    public List<Vector3> TileLocations { get => tileLocations; }
    public static StageManager Instance 
    {
        get 
        {
            instance.destructibles = GameObject.FindGameObjectWithTag("Destructible").GetComponent<Tilemap>();
            instance.tileLocations = new List<Vector3>();

            foreach (var pos in instance.destructibles.cellBounds.allPositionsWithin)
            {
                Vector3Int localPlace = new(pos.x, pos.y, pos.z);
                Vector3 place = instance.destructibles.CellToWorld(localPlace);
                if (instance.destructibles.HasTile(localPlace))
                {
                    instance.tileLocations.Add(place);
                }
            }

            return instance; 
        } 
    }


    protected override void LoadComponent()
    {
        instance = this;
    }

    public Vector3 FindClosestTile(Vector3 position)
    {
        Vector3 destination = Vector3.zero;
        float min = Mathf.Infinity;

        foreach (var pos in tileLocations)
        {
            if (Vector3.Distance(pos, position) < min)
            {
                destination = pos;
                min = Vector3.Distance(pos, position);
            }
        }

        destination.x = Mathf.Round(destination.x);
        destination.y = Mathf.Round(destination.y);

        return destination;
    }
}
