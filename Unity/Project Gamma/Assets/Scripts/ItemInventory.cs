using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    public List<Item> itemInventory;
    public Transform itemInventoryGUI;
    public SlotGUI[] slotGUIs;

    public Item CurrentPrimaryWeapon;
    public Item CurrentSecondWeapon;
    public Item CurrentMeleeWeapon;
    public EquipedWeaponGUI[] equipedWeaponGUIs;

    public bool isDiscardMode = false;
    public GameObject whenDiscardRedField;

    private void OnValidate()
    {
        slotGUIs = itemInventoryGUI.GetComponentsInChildren<SlotGUI>();
    }

    private void Awake()
    {
        UpdateItemInventory();
    }

    public void UpdateItemInventory()
    {
        int i = 0;
        for (; i < itemInventory.Count && i < slotGUIs.Length; i++)
        {
            slotGUIs[i].item = itemInventory[i];
        }
        for (; i < slotGUIs.Length; i++)
        {
            slotGUIs[i].item = null;
        }

        if (CurrentPrimaryWeapon == null)
        {
            equipedWeaponGUIs[0].item = null;
        }
        else
        {
            equipedWeaponGUIs[0].item = CurrentPrimaryWeapon;
        }

        if (CurrentSecondWeapon == null)
        {
            equipedWeaponGUIs[1].item = null;
        }
        else
        {
            equipedWeaponGUIs[1].item = CurrentSecondWeapon;
        }

        if (CurrentMeleeWeapon == null)
        {
            equipedWeaponGUIs[2].item = null;
        }
        else
        {
            equipedWeaponGUIs[2].item = CurrentMeleeWeapon;
        }
    }

    public void AddItem(Item item)
    {
        itemInventory.Add(item);
        UpdateItemInventory();
    }

    public void RemoveItem(Item item)
    {
        itemInventory.Remove(item);
        UpdateItemInventory();
    }

    public void UseItem(Item item)
    {
        Player player = GetComponentInParent<Player>();

        if (item.itemType == Item.ItemType.Potion)
        {
            player.Heal(item.hpRecoveryAmount);
        }
        else if (item.itemType == Item.ItemType.BodyArmor)
        {
            player.EquipArmor();
        }

        itemInventory.Remove(item);
        UpdateItemInventory();
    }

    public void EquipWeapon(Item item)
    {
        if (item.weaponType == Item.WeaponType.Pistol)
        {
            if (CurrentSecondWeapon == null)
            {
                CurrentSecondWeapon = item;
                itemInventory.Remove(item);
            }
        }
        else if (item.weaponType == Item.WeaponType.Melee)
        {
            if (CurrentMeleeWeapon == null)
            {
                CurrentMeleeWeapon = item;
                itemInventory.Remove(item);
            }
        }
        else
        {
            if (CurrentPrimaryWeapon == null)
            {
                CurrentPrimaryWeapon = item;
                itemInventory.Remove(item);
            }
        }

        UpdateItemInventory();
    }

    public void UnEquipWeapon(Item item)
    {
        if (item.weaponType == Item.WeaponType.Pistol)
        {
            itemInventory.Add(item);
            CurrentSecondWeapon = null;
        }
        else if (item.weaponType == Item.WeaponType.Melee)
        {
            itemInventory.Add(item);
            CurrentMeleeWeapon = null;
        }
        else
        {
            itemInventory.Add(item);
            CurrentPrimaryWeapon = null;
        }

        UpdateItemInventory();
    }

    public void OnClickDiscardItemBtn()     //called by OnAction of DiscardItemBtn
    {
        isDiscardMode = !isDiscardMode;

        if (isDiscardMode == true)
        {
            whenDiscardRedField.SetActive(true);
        }
        else
        {
            whenDiscardRedField.SetActive(false);
        }
    }
}
