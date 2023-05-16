using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickController : AbstractMonoBehaviour
{
    [Header("Brick Controller")]

    [SerializeField] protected float dropRate = 0.2f;
    [SerializeField] protected List<ItemScriptableObject> items = new();

    protected override void LoadComponent()
    {
        items = Resources.LoadAll<ItemScriptableObject>("Item/").ToList();
    }

    private void OnEnable()
    {
        StartCoroutine(SetInactive());
    }

    protected virtual IEnumerator SetInactive()
    {
        if (Random.value < dropRate)
        {
            int randomIndex = Random.Range(0, items.Count);
            Transform newItem = ItemSpawner.Instance.Spawn(items[randomIndex].itemName.ToString(), transform.position, transform.rotation);

            if (newItem.GetComponent<ItemController>() != null)
            {
                newItem.GetComponent<ItemController>().ItemName = items[randomIndex].itemName;
            }
        }

        yield return new WaitForSeconds(0.9f);


        gameObject.SetActive(false);
    }
}
