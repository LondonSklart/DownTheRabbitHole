using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    PlayerWeaponController weaponController;

    public string Name { get; set; }
    public List<BaseStat> Stats { get; set; }
    public string ObjectSlug { get; set; }
    public string ItemType { get; set; }
    public bool[] AOE { get; set; }


    public Item(string name,List <BaseStat> _Stats, string _ObjectSlug, string _ItemType, bool[] _AOE)
    {
        Name = name;
        Stats = _Stats;
        ObjectSlug = _ObjectSlug;
        ItemType = _ItemType;
        AOE = _AOE;

    }
    public void UseItem(Item itemToEquip)
    {
        weaponController = FindObjectOfType<PlayerWeaponController>();
        weaponController.EquipItem(itemToEquip);
    }

}

