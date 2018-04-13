using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPopUp : MonoBehaviour {

    public Text weaponName;
    public Text weaponDMG;
    public Text weaponHealth;
    public Text weaponINT;
    public Text weaponHaste;
    public Text weaponAOE;
    public Text weaponEffect;

    Vector3 location;

    private void FixedUpdate()
    {

        location = Input.mousePosition;
        
        
        gameObject.transform.position = location;
    }

    public void SetInfo(string weaponname,string weapondamage,string weaponhealth,string weaponinitiative,string weaponhaste,string weaponaoe,string weaponeffect)
    {
        weaponName.text =  weaponname;
        weaponDMG.text = "Damage: " + weapondamage;
        weaponHealth.text = "Health: " + weaponhealth;
        weaponINT.text = "Initiative: "+ weaponinitiative;
        weaponHaste.text = "Haste: "+ weaponhaste;
        weaponAOE.text = "Aoe: "+ weaponaoe;
        weaponEffect.text = "Effect: " +  weaponeffect;


    }





}
