using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public abstract class AbstractMonoBehaviour : MonoBehaviour
{
    protected virtual void Awake()
    {
        LoadComponent();
    }

    protected virtual void Reset()
    {
        LoadComponent();
    }

    protected abstract void LoadComponent();
}
