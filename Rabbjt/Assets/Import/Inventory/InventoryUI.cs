using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;

    InventoryController inventory;

    InventorySlot[] slots;


	// Use this for initialization
	void Start () {
        inventory = FindObjectOfType<InventoryController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.U))
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

        Debug.Log("Updating UI");
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.itemList.Count)
            {
                slots[i].AddItem(inventory.itemList[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
