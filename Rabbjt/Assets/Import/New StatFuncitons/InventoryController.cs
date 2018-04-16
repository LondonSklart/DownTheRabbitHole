using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    PlayerWeaponController weaponController;
    InventoryUI ui;

    public Transform itemSlotLocation;
    public GameObject inventorySlot;
    public GameMaster gameMaster;
    public WorldBuilder worldBuilder;

    public List<Item> itemList = new List<Item>();
    public List<Item> currentFloorItemList = new List<Item>();

    public delegate void OnItemAdded();
    public OnItemAdded OnitemAddedCallBack;

    private int coins = 10;
    public Text coinText;

    public Item sword;
    public Item helmet;
    public Item fist;

    public static InventoryController instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {

        ui = FindObjectOfType<InventoryUI>();
        weaponController = gameObject.GetComponent<PlayerWeaponController>();
        WeaponLibrary.Instance.GetLibraryLength();
        coinText.text = "Coin: " + coins;


        bool[] AOEchache = new bool[] {false,false,false,false };

        foreach (Equipment w in WeaponLibrary.Instance.GetAllWeapons())
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
            itemList.Add(new Item(w.name, statBlock, "sword", w.itemSlot, new Effect(w.dotName,w.dotDamage,w.hotRecovery,w.dotLength,w.dotIcon,w.armorShred,w.fragileInfliction),AOEchache));
            Instantiate(inventorySlot, itemSlotLocation);

            
        }
        OnLoadFloor();
        OnitemAddedCallBack.Invoke();
        //ui.UpdateUI();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponController.EquipItem(itemList[1]);

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            int length = weaponController.EquipedItems.Count;
            for (int i = 0; i < length; i++)
            {
                Debug.Log("Removing " + weaponController.EquipedItems[0]);

                weaponController.UnEquipItem(weaponController.EquipedItems[0]);
            }


            //foreach (Item i in weaponController.EquipedItems)
            //{
            //    weaponController.UnEquipItem(i);
            //}

        }
    }
    
    public void OnLoadFloor()
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
            currentFloorItemList.Add(new Item(w.name, statBlock, "sword", w.itemSlot,new Effect(w.dotName,w.dotDamage,w.hotRecovery,w.dotLength,w.dotIcon,w.armorShred,w.fragileInfliction),AOEchache));


        }
    }

    public int ReturnCoin()
    {
        return coins;
    }
    public void GetCoin(int coinToGet)
    {
        coins += coinToGet;
        coinText.text = "Coin: " + coins;
    }
    public void LoseCoin(int coinToLose)
    {
        coins -= coinToLose;
        coinText.text = "Coin: " + coins;
    }

    public void ChooseItem(Item item)
    {
        Debug.Log("boom");
        Instantiate(inventorySlot, itemSlotLocation);
        itemList.Add(item);
        OnitemAddedCallBack.Invoke();
        //ui.UpdateUI();
    }
}
