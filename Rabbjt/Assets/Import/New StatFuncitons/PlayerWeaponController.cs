﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquipedWeapon { get; set; }

    public GameObject playerHead;
    public GameObject EquipedHelmet { get; set; }

    public Equipment EquipedOldWeapon { get; set; }


    public List<Item> EquipedItems = new List<Item>();


    CharacterStats characterStat;
    GameObject location;
    PlayerController player;

    IWeapon equipedWeapon;

    private void Start()
    {
        characterStat = GetComponent<CharacterStats>();
        player = GetComponent<PlayerController>();
    }



    public void EquipItem(Item itemToEquip)
    {
        player.startingHealth += itemToEquip.Stats[1].GetCalculatedValue();
        player.health += itemToEquip.Stats[1].GetCalculatedValue();




        switch (itemToEquip.Slot)
        {
            case EquipmenSlot.Weapon:

                if (EquipedWeapon != null)
                {
                    player.startingHealth -= EquipedWeapon.GetComponent<IWeapon>().Stats[1].GetCalculatedValue();
                    player.health -= EquipedWeapon.GetComponent<IWeapon>().Stats[1].GetCalculatedValue();

                    characterStat.RemoveStatBonus(EquipedWeapon.GetComponent<IWeapon>().Stats);
                    Destroy(playerHand.transform.GetChild(0).gameObject);
                }


                location =playerHand;
                EquipedWeapon = Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), location.transform.position, location.transform.rotation);
                equipedWeapon = EquipedWeapon.GetComponent<IWeapon>();
                EquipedWeapon.GetComponent<IWeapon>().Stats = itemToEquip.Stats;
                EquipedWeapon.transform.SetParent(playerHand.transform);
                break;
            case EquipmenSlot.Head:


                if (EquipedHelmet != null)
                {
                    player.startingHealth -= EquipedHelmet.GetComponent<IWeapon>().Stats[1].GetCalculatedValue();
                    player.health -= EquipedHelmet.GetComponent<IWeapon>().Stats[1].GetCalculatedValue();

                    characterStat.RemoveStatBonus(EquipedHelmet.GetComponent<IWeapon>().Stats);
                    Destroy(playerHead.transform.GetChild(0).gameObject);
                }



                location = playerHead;
                EquipedHelmet = Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), location.transform.position, location.transform.rotation);

                EquipedHelmet.GetComponent<IWeapon>().Stats = itemToEquip.Stats;
                EquipedHelmet.transform.SetParent(playerHead.transform);

                break;
        }


        EquipedItems.Add(itemToEquip);
        characterStat.AOE = itemToEquip.AOE;
        characterStat.AddStatBonus(itemToEquip.Stats);
    }






    public void UnEquipItem(Item itemToUnequip)
    {


        switch (itemToUnequip.Slot)
        {
            case EquipmenSlot.Weapon:
                if (EquipedWeapon!=null)
                {

                    characterStat.RemoveStatBonus(EquipedWeapon.GetComponent<IWeapon>().Stats);
                    Destroy(playerHand.transform.GetChild(0).gameObject);
                    EquipedWeapon = null;
                    player.startingHealth -= itemToUnequip.Stats[1].GetCalculatedValue();
                    player.health -= itemToUnequip.Stats[1].GetCalculatedValue();

                }
                break;
            case EquipmenSlot.Head:
                if (EquipedHelmet)
                {
                    characterStat.RemoveStatBonus(EquipedHelmet.GetComponent<IWeapon>().Stats);
                    Destroy(playerHead.transform.GetChild(0).gameObject);
                    EquipedHelmet = null;
                    player.startingHealth -= itemToUnequip.Stats[1].GetCalculatedValue();
                    player.health -= itemToUnequip.Stats[1].GetCalculatedValue();
                }
                break;

        }


    }

    public void ClearEquipedItemsList()
    {
        EquipedItems.Clear();
    }


    public void PerformAttack()
    {
        equipedWeapon.PerformAttack();
    }
}
