using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PlayerWeaponController weaponController;
    PlayerController playerController;

    List<Item> itemList = new List<Item>();

    public Item sword;
    public Item helmet;
    public Item fist;

    private void Start()
    {
        weaponController = gameObject.GetComponent<PlayerWeaponController>();
        playerController = FindObjectOfType<PlayerController>();
        WeaponLibrary.Instance.GetLibraryLength();



        for (int i = 0; i < WeaponLibrary.Instance.GetAllWeapons().Length; i++)
        {

            List<BaseStat> statBlock = new List<BaseStat>();
            statBlock.Add(new BaseStat(WeaponLibrary.Instance.GetWeapon(i).power, "Power", "Your power level"));
            statBlock.Add(new BaseStat(WeaponLibrary.Instance.GetWeapon(i).health, "Health", "Your health level"));
            statBlock.Add(new BaseStat(WeaponLibrary.Instance.GetWeapon(i).initiative, "Initiative", "Your initiative level"));
            statBlock.Add(new BaseStat(WeaponLibrary.Instance.GetWeapon(i).haste, "Haste", "Your haste level"));

               itemList.Add( new Item(statBlock, "sword", "Weapon" ,WeaponLibrary.Instance.GetWeapon(i).AOE));
        }






        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(1, "Power", "Your power level"));
        swordStats.Add(new BaseStat(1, "Health", "Your health level"));
        bool[] swordaoe = new bool[] { true, true, false, false };
        sword = new Item(swordStats, "sword","Weapon",swordaoe);


        List<BaseStat> helmetStats = new List<BaseStat>();
        helmetStats.Add(new BaseStat(1, "Power", "Your power level"));
        helmetStats.Add(new BaseStat(1, "Health", "Your health level"));
        bool[] helmetaoe = new bool[] { false, false, false, false };
        helmet = new Item(helmetStats, "helmet", "Helmet",helmetaoe);

        List<BaseStat> fistStats = new List<BaseStat>();
        fistStats.Add(new BaseStat(1, "Power", "Your power level"));
        fistStats.Add(new BaseStat(1, "Health", "Your health level"));
        bool[] fistaoe = new bool[] { true, false, false, false };
        fist = new Item(fistStats, "helmet", "Weapon",fistaoe);

        weaponController.EquipItem(itemList[0]);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponController.EquipItem(itemList[1]);

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // weaponController.UnEquipItem(sword);
            weaponController.EquipItem(helmet);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            weaponController.UnEquipItem(itemList[1]);
            weaponController.UnEquipItem(helmet);

        }
    }



}
