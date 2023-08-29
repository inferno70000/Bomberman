using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : AbstractMonoBehaviour
{
    [Header("Movement")]

    [SerializeField] protected float speed = 5f;

    protected new Rigidbody2D rigidbody2D;
    protected CharacterController characterController;

    public float Speed { get => speed; }

    protected override void LoadComponent()
    {
        if (rigidbody2D == null)
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }

    void FixedUpdate()
    {
        if (!characterController.IsDead)
        {
            Move();
        }
    }

    protected virtual void Move()
    {
        Vector2 position = Speed * Time.fixedDeltaTime * InputManager.Instance.GetMovementDirection();
        rigidbody2D.MovePosition((Vector2)transform.position + position);
    }

    public virtual void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
