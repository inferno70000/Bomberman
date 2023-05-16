using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : AbstractMonoBehaviour
{
    [Header("Bomb Controller")]

    [SerializeField] protected CircleCollider2D circleCollider2D;
    [SerializeField] protected float timeFuse = 3f;
    [SerializeField] private int length = 2;

    protected int Length { get => length; }

    private void OnEnable()
    {
        StartCoroutine(Fuse());
    }

    protected override void LoadComponent()
    {
        if (circleCollider2D == null)
        {
            circleCollider2D = GetComponent<CircleCollider2D>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            circleCollider2D.isTrigger = false;
        }
    }

    protected virtual IEnumerator Fuse()
    {
        yield return new WaitForSeconds(timeFuse);

        ExplosionSpawner.Instance.Explode(Length, transform.position, transform.rotation);

        transform.gameObject.SetActive(false);
    }

    public virtual void SetCircleCollider2D(CircleCollider2D circleCollider2D)
    {
        this.circleCollider2D = circleCollider2D;
    }

    public virtual CircleCollider2D GetCircleCollider2D()
    {
        return circleCollider2D;
    }

    public virtual void SetLength(int length)
    {
        this.length = length;
    }
}
