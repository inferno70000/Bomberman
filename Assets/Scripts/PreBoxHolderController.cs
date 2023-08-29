using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBoxHolderController : MonoBehaviour
{
    [Header("Pre Box Holder Controller")]
    [SerializeField]
    private List<PreExplosionBoxController> explosionBoxes;

    private void OnEnable()
    {
        StartCoroutine(SetInactive());
    }

    private void OnDisable()
    {
        explosionBoxes.Clear();
    }

    private void Update()
    {
        //bool isEmpty = (bool)!explosionBoxes?.Any();

        if (explosionBoxes.Count == 0)
        {
            explosionBoxes = new(GetComponentsInChildren<PreExplosionBoxController>());
        }
        else
        {
            //if (CheckTrigger() == false)
            //{
            //    foreach (var item in explosionBoxes)
            //    {
            //        item.SetObstacle(true);
            //    }

            if (CheckOverLap() == false)
            {
                foreach (var item in explosionBoxes)
                {
                    item.SetObstacle(true);
                }
            }
        }
    }

    protected virtual IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(4f);

        gameObject.SetActive(false);
    }

    //protected bool CheckTrigger()
    //{
    //    foreach(var item in explosionBoxes)
    //    {
    //        if (item.IsTrigger)
    //        {
    //            return true;
    //        }
    //    }

    //    return false;
    //}

    protected bool CheckOverLap()
    {
        int count = 0;
        foreach (var item in explosionBoxes)
        {
            if (item.IsOverlap)
            {
                count++;
            }
        }

        Debug.Log(count);

        if (count == 0) { Debug.Log("not overlap"); return false; }

        return true;
    }
}
