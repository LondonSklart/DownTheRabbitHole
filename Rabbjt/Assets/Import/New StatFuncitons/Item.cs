﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    PlayerWeaponController weaponController;
    InventoryController inventoryController;

    public string Name { get; set; }
    public List<BaseStat> Stats { get; set; }
    public string ObjectSlug { get; set; }
    public EquipmenSlot Slot { get; set; }
    public Effect OnHitEffect { get; set; }
    public bool[] AOE { get; set; }


    public Item(string name,List <BaseStat> _Stats, string _ObjectSlug, EquipmenSlot _Slot, Effect _OnHit,bool[] _AOE)
    {
        Name = name;
        Stats = _Stats;
        ObjectSlug = _ObjectSlug;
        Slot = _Slot;
        AOE = _AOE;
        OnHitEffect = _OnHit;

    }
    public void UseItem(Item itemToEquip, PlayerWeaponController pwc)
    {
        weaponController = pwc;
        weaponController.EquipItem(itemToEquip);
    }

    public void ChooseItem (Item itemToAdd, InventoryController ic)
    {
        inventoryController = ic;
        inventoryController.ChooseItem(itemToAdd);
        GameObject.FindGameObjectWithTag("LootScreen").SetActive(false);

    }

}

