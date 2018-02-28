using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject EquipedWeapon { get; set; }

    public GameObject playerHead;
    public GameObject EquipedHelmet { get; set; }

    CharacterStats characterStat;
    GameObject location;


    IWeapon equipedWeapon;

    private void Start()
    {
        characterStat = GetComponent<CharacterStats>();
    }


    public void EquipItem(Item itemToEquip)
    {

       /* if (EquipedWeapon != null)
        {
            characterStat.RemoveStatBonus(EquipedWeapon.GetComponent<IWeapon>().Stats);
            Destroy(playerHand.transform.GetChild(0).gameObject);
        }*/


        switch (itemToEquip.ItemType)
        {
            case "Weapon":
                location=playerHand;
                EquipedWeapon = Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), location.transform.position, location.transform.rotation);
                equipedWeapon = EquipedWeapon.GetComponent<IWeapon>();
                EquipedWeapon.GetComponent<IWeapon>().Stats = itemToEquip.Stats;
                EquipedWeapon.transform.SetParent(playerHand.transform);

                break;
            case "Helmet":
                location = playerHead;
                EquipedHelmet = Instantiate(Resources.Load<GameObject>("Weapons/" + itemToEquip.ObjectSlug), location.transform.position, location.transform.rotation);

                EquipedHelmet.GetComponent<IWeapon>().Stats = itemToEquip.Stats;
                EquipedHelmet.transform.SetParent(playerHead.transform);

                break;
        }




        characterStat.AddStatBonus(itemToEquip.Stats);
    }
    public void UnEquipItem(Item itemToUnequip)
    {
        switch (itemToUnequip.ItemType)
        {
            case "Weapon":
                if (EquipedWeapon != null)
                {
                    characterStat.RemoveStatBonus(EquipedWeapon.GetComponent<IWeapon>().Stats);
                    Destroy(playerHand.transform.GetChild(0).gameObject);
                }
                break;
            case "Helmet":
                if (EquipedHelmet != null)
                {
                    characterStat.RemoveStatBonus(EquipedHelmet.GetComponent<IWeapon>().Stats);
                    Destroy(playerHead.transform.GetChild(0).gameObject);
                }
                break;

        }

    }


    public void PerformAttack()
    {
        equipedWeapon.PerformAttack();
    }
}
