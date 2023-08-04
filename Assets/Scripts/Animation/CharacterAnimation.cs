using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : AbstractMonoBehaviour
{
    [Header("Character Animation")]

    [SerializeField] Animator animator;
    [SerializeField] CharacterController characterController;

    protected override void LoadComponent()
    {
        animator = GetComponentInChildren<Animator>();    
        characterController = GetComponent<CharacterController>();
    }

    protected virtual void Animate(Vector2 direction)
    {
        if (characterController.IsDead)
        {
            animator.Play("Die");
            animator.SetFloat("MoveX", -2);
            animator.SetFloat("MoveY", -2);
            animator.SetBool("IsDead", characterController.IsDead);

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

    protected virtual IEnumerator SetInactive()
    {
        yield return new WaitForSeconds(0.9f);

        gameObject.SetActive(false);
    }
}
