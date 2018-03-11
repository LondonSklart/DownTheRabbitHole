using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PlayerWeaponController weaponController;
    PlayerController playerController;
    InventoryUI ui;

    public Transform itemSlotLocation;
    public GameObject inventorySlot;

    public List<Item> itemList = new List<Item>();

    public delegate void OnItemAdded();
    public static OnItemAdded OnitemAddedCallBack; 

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
        playerController = FindObjectOfType<PlayerController>();
        WeaponLibrary.Instance.GetLibraryLength();



        bool[] AOEchache = new bool[] {false,false,false,false };

        foreach (Equipment w in WeaponLibrary.Instance.GetAllWeapons())
        {
            Debug.Log("ay");
            List<BaseStat> statBlock = new List<BaseStat>();
            statBlock.Add(new BaseStat(w.power, "Power", "Your power level"));
            statBlock.Add(new BaseStat(w.health, "Health", "Your health level"));
            statBlock.Add(new BaseStat(w.initiative, "Initiative", "Your initiative level"));
            statBlock.Add(new BaseStat(w.haste, "Haste", "Your haste level"));
            if (w.itemSlot == EquipmenSlot.Weapon)
            {
                AOEchache = w.AOE;
            }
            itemList.Add(new Item(w.name, statBlock, "sword", w.itemSlot, AOEchache));
            Instantiate(inventorySlot, itemSlotLocation);

            
        }

        ui.UpdateUI();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponController.EquipItem(itemList[1]);

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            foreach (Item i in weaponController.EquipedItems)
            {
                weaponController.UnEquipItem(i);
            }

        }
    }

}
