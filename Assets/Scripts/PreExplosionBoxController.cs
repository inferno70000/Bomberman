using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshObstacle))]

public class PreExplosionBoxController : AbstractMonoBehaviour
{
    [Header("Pre-Explosion Box Controller")]
    private NavMeshObstacle obstacle;
    private void OnEnable()
    {
        StartCoroutine(SetInactive());
    }

    protected virtual IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(3.9f);

        transform.gameObject.SetActive(false);
    }

    private void Start()
    {
        LayerMask botMask = LayerMask.GetMask("Bot");

        //turn on obstacle if didn't overlap with bot
        if(!Physics2D.OverlapBox(transform.position, Vector2.one / 2f, 0f, botMask))
        {
            obstacle.enabled = true;
        }
    }

    protected override void LoadComponent()
    {
        obstacle = GetComponent<NavMeshObstacle>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bot"))
        {
            obstacle.enabled = true;
        }
    }

}
