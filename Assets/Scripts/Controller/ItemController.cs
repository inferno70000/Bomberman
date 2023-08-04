using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemController : MonoBehaviour
{
    [SerializeField] protected ItemName itemName = ItemName.None;

    public ItemName ItemName { get => itemName; set => itemName = value; }

    protected virtual void SetAttribute(CharacterController characterController)
    {
        if (characterController == null)
        {
            return;
        }

        switch(itemName)
        {
            case ItemName.ItemBlastRadius:
                characterController.SetBombRadius(characterController.BombRadius + 1);
                break;
            case ItemName.ItemExtraBomb:
                characterController.SetBombAmount(characterController.BombAmount + 1);
                break;
            case ItemName.ItemSpeedIncrease:
                Debug.Log("Speed item");
                characterController.SetSpeed(characterController.Speed + 1);
                break;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Bot"))
        {
            SetAttribute(collision.GetComponent<CharacterController>());
        }
    }
}
