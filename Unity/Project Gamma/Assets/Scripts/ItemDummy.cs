using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDummy : MonoBehaviour
{
    public Item item;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player.isUseBtnPressed == true)
            {
                ItemInventory itemInventory = other.GetComponentInChildren<ItemInventory>();

                if (itemInventory.itemInventory.Count < itemInventory.slotGUIs.Length)
                {
                    itemInventory.AddItem(item);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Inventory is Full!");
                }
            }
        }
    }
}
