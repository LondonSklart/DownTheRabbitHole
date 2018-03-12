using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public Transform lootParent;

    public GameObject LootScreen;

    public InventoryController inventory;

    InventorySlot[] slots;


	// Use this for initialization
	void Start () {
       // inventory = FindObjectOfType<InventoryController>();
        InventoryController.OnitemAddedCallBack += UpdateUI;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UpdateUI();
        }
    }

    public void UpdateUI()
    {
        Debug.Log("adding intem");

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();

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
    public void VictoryUI()
    {
        InventorySlot[] victoryLootSlots;
        LootScreen.SetActive(true);
        Debug.Log("Victory!");
        victoryLootSlots = lootParent.GetComponentsInChildren<InventorySlot>();
        Debug.Log(victoryLootSlots.Length);
        for (int i = 0; i < victoryLootSlots.Length; i++)
        {

                victoryLootSlots[i].AddItem(inventory.itemList[i]);
 
        }
    }
}
