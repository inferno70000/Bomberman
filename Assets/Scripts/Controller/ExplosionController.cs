using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : AbstractMonoBehaviour
{
    [Header("Explosion Controller")]

    [SerializeField] private Animator animator;

    protected override void LoadComponent()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    private void OnEnable()
    {
        StartCoroutine(SetInactive());
    }

    protected virtual IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(0.9f);

        transform.gameObject.SetActive(false);
    }

    public virtual void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);

        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().SetIsDead(true);
        }
    }
}
