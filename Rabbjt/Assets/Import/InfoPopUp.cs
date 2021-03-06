﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour {

    public Text weaponName;
    public Text weaponDMG;
    public Text weaponHealth;
    public Text weaponArmor;
    public Text weaponINT;
    public Text weaponHaste;
    public Text weaponAOE;
    public Text weaponEffect;
    public Text weaponValue;

    Vector3 location;

    private void FixedUpdate()
    {

        location = Input.mousePosition;
        
        
        gameObject.transform.position = location;
    }

    public void SetInfo(string weaponname,string weapondamage,string weaponarmor,string weaponhealth,string weaponinitiative,string weaponhaste,string weaponaoe,string weaponeffect,string weaponvalue)
    {
        weaponName.text =  weaponname;
        weaponDMG.text = "Damage: " + weapondamage;
        weaponArmor.text = "Armor: " + weaponarmor;
        weaponHealth.text = "Health: " + weaponhealth;
        weaponINT.text = "Initiative: "+ weaponinitiative;
        weaponHaste.text = "Haste: "+ weaponhaste;
        weaponAOE.text = "Aoe: "+ weaponaoe;
        weaponEffect.text = "Effect: " +  weaponeffect;
        weaponValue.text = weaponvalue;

    }





}
