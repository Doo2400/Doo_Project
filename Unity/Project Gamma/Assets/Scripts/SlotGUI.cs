using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotGUI : MonoBehaviour
{
    public GameObject player;
    public GameObject itemDropPos;
    public Image image;
    private Item itemInSlot;
    public Item item
    {

        get { return itemInSlot; }

        set
        {
            itemInSlot = value;

            if (itemInSlot != null)
            {
                image.sprite = item.itemImage;
                image.color = new Color(1, 1, 1, 1);
            }
            else   //itemInSlot == null
            {
                image.color = new Color(1, 1, 1, 0);
            }
        }
    }

    public void OnClickSlotGUI()    // called by Envent Trigger
    {
        ItemInventory itemInventory = player.GetComponentInChildren<ItemInventory>();

        if (item == null)
        {
            return;
        }

        if (itemInventory.isDiscardMode == false)
        {
            if (item.itemType == Item.ItemType.Weapon)
            {
                itemInventory.EquipWeapon(item);
            }
            else if (item.itemType == Item.ItemType.Potion || item.itemType == Item.ItemType.BodyArmor)
            {
                itemInventory.UseItem(item);
            }
        }
        else
        {
            Instantiate(item.itemShape3D, itemDropPos.transform.position, transform.rotation);

            itemInventory.RemoveItem(item);
        }
    }
}
