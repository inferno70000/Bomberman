using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]
public class PreExplosionBoxController : AbstractMonoBehaviour
{
    [Header("Pre-Explosion Box Controller")]
    private NavMeshObstacle obstacle;
    private LayerMask botMask;
    //private bool isTrigger;
    private bool isOverlap;

    //public bool IsTrigger { get => isTrigger; }
    public bool IsOverlap { get => isOverlap;  }

    protected override void LoadComponent()
    {
        botMask = LayerMask.GetMask("Bot");
        obstacle = GetComponent<NavMeshObstacle>();
    }

    private void OnEnable()
    {
        StartCoroutine(SetInactive());
    }

    //private void OnDisable()
    //{
    //    isTrigger = true;
    //}

    protected virtual IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(3.9f);

        transform.SetParent(PreExplosionSpawner.Instance.transform.Find("Holder"));
        //obstacle.enabled = false;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Physics2D.OverlapBox(transform.position, Vector2.one / 2f, 0f, botMask))
        {
            isOverlap = true;
        }
        else
        {
            isOverlap = false;
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Bot"))
    //    {
    //        isTrigger = false;
    //    }
    //}
    
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Bot"))
    //    {
    //        isTrigger = true;
    //    }
    //}

    /// <summary>
    /// Set active status of NavMesh Obstacle
    /// </summary>
    public void SetObstacle(bool isActive)
    {
        obstacle.enabled = isActive;
    }
}
