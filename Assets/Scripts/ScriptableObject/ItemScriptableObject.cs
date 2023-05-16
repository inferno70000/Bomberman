using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item")]
public class ItemScriptableObject : ScriptableObject
{
    [SerializeField] public ItemName itemName = ItemName.None;
}


[Serializable]
public enum ItemName
{
    None,
    ItemBlastRadius,
    ItemExtraBomb,
    ItemSpeedIncrease,
}

