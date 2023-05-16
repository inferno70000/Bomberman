using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : AbstractMonoBehaviour
{
    [Header("Movement")]

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected new Rigidbody2D rigidbody2D;
    [SerializeField] protected Animator animator;
    [SerializeField] protected PlayerController playerController; 

    public float Speed { get => speed; }

    protected override void LoadComponent()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }

        if (playerController == null)
        {
            playerController = GetComponent<PlayerController>();
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        Vector2 position = Speed * Time.fixedDeltaTime * InputManager.Instance.GetMovementDirection();
        rigidbody2D.MovePosition((Vector2)transform.position + position);
        
        Animate();
    }

    protected virtual void Animate()
    {
        Vector2 direction = InputManager.Instance.GetMovementDirection();
        if (playerController.IsDead)
        {
            animator.Play("WhiteDie");
            StartCoroutine(SetInactive());
        }
        else if (direction == Vector2.zero)
        {
            animator.Play("Idle");
            return;
        }
        else
        {
            animator.Play("Walk");
        }
        animator.SetFloat("MoveX", direction.x);
        animator.SetFloat("MoveY", direction.y);
    }

    public virtual void SetSpeed(float speed)
    {
        this.speed = speed; 
    }

    protected virtual IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(0.9f);

        gameObject.SetActive(false);   
    }
}
