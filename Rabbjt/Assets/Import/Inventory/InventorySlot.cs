using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Text itemName;

    Item item;

    public void AddItem(Item newItem)
    {
   
            item = newItem;
            itemName.text = newItem.Name;
        

    }



    public void ClearSlot()
    {
        Destroy(gameObject);
    }

    public Item GetItem()
    {
        return item;
    }

    public void UseItem()
    {

        item.UseItem(item, PlayerWeaponController.instance);
    }
    public void ChooseItem()
    {
        item.ChooseItem(item, InventoryController.instance);
    }
}
