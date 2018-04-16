using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    public Transform itemsParent;
    public Transform lootParent;

    public GameObject LootScreen;
    public Text coinText;

    public GameMaster gameMaster;
    public WorldBuilder worldBuilder;

    public InventoryController inventory;

    InventorySlot[] slots;

    private LootTable currentLootTable;
    int randomNumber;

    // Use this for initialization
    void Start () {
        inventory = FindObjectOfType<InventoryController>();
        inventory.OnitemAddedCallBack += UpdateUI;
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
        currentLootTable = gameMaster.lootTables[worldBuilder.GetCurrentFloor()];

        List<int> chachedNumbers = new List<int>();

        InventorySlot[] victoryLootSlots;
        LootScreen.SetActive(true);
        coinText.text = "Coin Looted " + FindObjectOfType<TurnManager>().GetCoinReward();
        Debug.Log("Victory!");
        victoryLootSlots = lootParent.GetComponentsInChildren<InventorySlot>();
        Debug.Log(victoryLootSlots.Length);
        for (int i = 0; i < victoryLootSlots.Length; i++)
        {
            randomNumber = Random.Range(0,currentLootTable.GetLibraryLength());
            while (chachedNumbers.Contains(randomNumber))
            {
                randomNumber = Random.Range(0, currentLootTable.GetLibraryLength());
            }
            victoryLootSlots[i].AddItem(inventory.currentFloorItemList[randomNumber]);
            chachedNumbers.Add(randomNumber);



 
        }
    }

}
