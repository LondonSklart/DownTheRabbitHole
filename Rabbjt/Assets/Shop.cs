using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

    public Transform itemSlotLocation;
    public GameObject inventorySlot;
    public GameMaster gameMaster;
    public WorldBuilder worldBuilder;


    InventorySlot[] slots;

    List<Item> shopItems = new List<Item>();


    private void Awake()
    {
        OnShopLoad();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            SlotLoad();
        }
    }

    public void OnShopLoad()
    {

        bool[] AOEchache = new bool[] { false, false, false, false };

        foreach (Equipment w in gameMaster.lootTables[worldBuilder.GetCurrentFloor()].GetAllWeapons())
        {
            List<BaseStat> statBlock = new List<BaseStat>();
            statBlock.Add(new BaseStat(w.power, "Power", "Your power level"));
            statBlock.Add(new BaseStat(w.health, "Health", "Your health level"));
            statBlock.Add(new BaseStat(w.initiative, "Initiative", "Your initiative level"));
            statBlock.Add(new BaseStat(w.haste, "Haste", "Your haste level"));
            if (w.itemSlot == EquipmenSlot.Weapon)
            {
                AOEchache = w.AOE;
            }
            shopItems.Add(new Item(w.name, statBlock, "sword", w.itemSlot, new Effect(w.dotName, w.dotDamage, w.hotRecovery, w.dotLength, w.dotIcon, w.armorShred, w.fragileInfliction), AOEchache));
            Instantiate(inventorySlot, itemSlotLocation);


        }


    }
    public void SlotLoad()
    {
        slots = itemSlotLocation.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shopItems.Count)
            {
                slots[i].AddItem(shopItems[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
}
