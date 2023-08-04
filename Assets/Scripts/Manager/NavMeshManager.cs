using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;

public class NavMeshManager : AbstractMonoBehaviour
{
    [Header("NavMesh Manager")]
    
    [SerializeField] NavMeshSurface surface2D;

    private static NavMeshManager instance; 
    public static NavMeshManager Instance {  get { return instance; } }

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
        {
            instance = this;
        }
    }

    protected override void LoadComponent()
    {
        if (surface2D == null)
        {
            surface2D = GetComponent<NavMeshSurface>();
        }
    }

    public void BakeNav()
    {
        surface2D.BuildNavMesh();
    }
}
