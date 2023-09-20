using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipedWeaponGUI : MonoBehaviour
{
    public GameObject player;
    public Image image;
    private Item itemInSlot;
    public Item item
    {
        get { return itemInSlot; }

        set {
            itemInSlot = value;

            if(itemInSlot != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void OnClickSlotGuI()
    {
        ItemInventory itemInventory = player.GetComponentInChildren<ItemInventory>();

        if(item == null)
        {
            return;
        }

        itemInventory.UnEquipWeapon(item);
    }

}
