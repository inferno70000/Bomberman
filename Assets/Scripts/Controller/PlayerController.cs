using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{
    [Header("Player")]
    [SerializeField] private bool isInvincible = false;

    private void Update()
    {
        if (InputManager.Instance.GetBombKey())
        {
            PlaceBomb();
        }
    }

    public override void SetSpeed(float speed)
    {
        this.speed = speed;

        Debug.Log(speed);

        movement.SetSpeed(speed);
    }

    public override void SetIsDead(bool isDead)
    {
        if (isInvincible)
        {
            return;
        }

        base.SetIsDead(isDead);
    }
}
