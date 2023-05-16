using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemController : MonoBehaviour
{
    [SerializeField] protected ItemName itemName = ItemName.None;

    public ItemName ItemName { get => itemName; set => itemName = value; }

    protected virtual void SetAttribute(PlayerController playerController)
    {
        if (playerController == null)
        {
            return;
        }

        switch(itemName)
        {
            case ItemName.ItemBlastRadius:
                playerController.SetBombRadius(playerController.BombRadius + 1);
                break;
            case ItemName.ItemExtraBomb:
                playerController.SetBombAmount(playerController.BombAmount + 1);
                break;
            case ItemName.ItemSpeedIncrease:
                playerController.SetSpeed(playerController.Speed + 1);
                break;
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SetAttribute(collision.GetComponent<PlayerController>());
        }
    }
}
