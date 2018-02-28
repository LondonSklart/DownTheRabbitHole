using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    PlayerWeaponController weaponController;
    public Item sword;
    public Item helmet;

    private void Start()
    {
        weaponController = gameObject.GetComponent<PlayerWeaponController>();
        List<BaseStat> swordStats = new List<BaseStat>();
        swordStats.Add(new BaseStat(6, "Power", "Your power level"));
        swordStats.Add(new BaseStat(6, "Health", "Your health level"));
        sword = new Item(swordStats, "sword","Weapon");

        List<BaseStat> helmetStats = new List<BaseStat>();
        helmetStats.Add(new BaseStat(1, "Power", "Your power level"));
        helmetStats.Add(new BaseStat(1, "Health", "Your health level"));
        helmet = new Item(helmetStats, "helmet", "Helmet");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weaponController.EquipItem(sword);

        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            // weaponController.UnEquipItem(sword);
            weaponController.EquipItem(helmet);

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            weaponController.UnEquipItem(sword);
            weaponController.UnEquipItem(helmet);

        }
    }



}
