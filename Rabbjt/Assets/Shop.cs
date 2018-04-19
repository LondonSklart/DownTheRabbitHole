using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public Transform itemSlotLocation;
    public GameObject inventorySlot;
    public GameMaster gameMaster;
    public WorldBuilder worldBuilder;

    public Text SellText;

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
            statBlock.Add(new BaseStat(w.armor, "Armor", "Your armor level"));

            if (w.itemSlot == EquipmenSlot.Weapon)
            {
                AOEchache = w.AOE;
            }
            shopItems.Add(new Item(w.name, statBlock, w.weaponModel, w.itemSlot, new Effect(w.dotName,w.dotAffectSelf, w.dotDamage, w.hotRecovery, w.dotLength, w.dotIcon, w.armorShred, w.fragileInfliction), AOEchache,w.value));
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
    public void ShopRecieveItem(Item item)
    {
        shopItems.Add(item);
        Instantiate(inventorySlot, itemSlotLocation);
        SlotLoad();
    }
    public void CloseShop()
    {
        gameObject.SetActive(false);
    }
    public void Sell()
    {
        SellText.text = "What item do you want to sell?";
        InventoryController.instance.SetSellMode(true);

    }
    public void SetSellMode(bool mode)
    {
        InventoryController.instance.SetSellMode(mode);
    }
    public List<Item> GetShopStock()
    {
        return shopItems;
    }
}
