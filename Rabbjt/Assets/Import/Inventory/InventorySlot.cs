using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Text itemType;

    Item item;

    public void AddItem(Item newItem)
    {
        item = newItem;
        itemType.text = newItem.ItemType;
    }

    public void ClearSlot()
    {
        Debug.Log("ClearingSlot");
        Destroy(gameObject);
    }
}
