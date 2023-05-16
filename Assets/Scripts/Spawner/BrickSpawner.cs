using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSpawner : Spawner
{
    [Header("Brick Spawner")]

    private static BrickSpawner instance;
    public static BrickSpawner Instance { get => instance; }
    public static string BRICK = "Brick";

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Only 1 BrickSpawner allow to exist");
        }

        instance = this;
    }
}
