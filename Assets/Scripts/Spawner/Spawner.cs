using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : AbstractMonoBehaviour
{
    [Header("Spawner")]

    [SerializeField] protected List<Transform> prefabs = new();
    [SerializeField] protected List<Transform> holder = new();

    protected override void LoadComponent()
    {
        LoadPrefabs();
    }

    protected virtual void LoadPrefabs()
    {
        Transform prefabsTransform = transform.Find("Prefabs");

        if (prefabs.Count != prefabsTransform.childCount)
        {
            prefabs.Clear();
            foreach(Transform child in prefabsTransform)
            {
                prefabs.Add(child.transform);
            }
        }
    }

    //Spawn first game object in prefabs
    public virtual Transform Spawn(Vector2 position, Quaternion rotation)
    {
        Transform newPrefab = Spawn(prefabs[0].name, position, rotation);

        return newPrefab;
    }

    //Spawn game object with specific name
    public virtual Transform Spawn(string prefabName, Vector2 position, Quaternion rotation)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Transform newPrefab = GetPrefabByName(prefabName);

        SetPrefab(newPrefab, prefabName, position, rotation);

        return newPrefab;
    }  
    
    public virtual Transform Spawn(string prefabName, Transform parent, Vector2 position, Quaternion rotation)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Transform newPrefab = GetPrefabByName(prefabName);

        SetPrefab(newPrefab, parent, prefabName, position, rotation);

        return newPrefab;
    }

    protected virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in holder)
        {
            if (!prefab.gameObject.activeSelf && prefab.name == prefabName)
            {
                return prefab;
            }
        }

        return CreatePrefab(prefabName);
    }

    protected virtual Transform CreatePrefab(string prefabName)
    {

        Transform prefab = null;
        foreach (Transform item in prefabs)
        {
            if (item.name == prefabName)
            {
                prefab = item;
                break;
            }
        }

        Transform newPrefab = Instantiate(prefab);

        holder.Add(newPrefab);  

        return newPrefab;
    }

    protected virtual void SetPrefab(Transform prefab, string prefabName, Vector2 position, Quaternion rotation)
    {
        prefab.name = prefabName;   
        prefab.SetPositionAndRotation(position, rotation);
        prefab.gameObject.SetActive(true);
        prefab.SetParent(transform.Find("Holder"));
    }
    
    protected virtual void SetPrefab(Transform prefab, Transform parent, string prefabName, Vector2 position, Quaternion rotation)
    {
        SetPrefab(prefab, prefabName, position, rotation);
        prefab.SetParent(parent);
    }

}
