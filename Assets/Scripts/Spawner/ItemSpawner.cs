using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : Spawner
{
    [Header("Item Spawner")]

    private static ItemSpawner instance;
    public static ItemSpawner Instance { get => instance; }

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Only 1 ItemSpawner allow to exist");
        }

        instance = this;
    }

    
}
