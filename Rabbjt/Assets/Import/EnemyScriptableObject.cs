using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Equipment : ScriptableObject
{
    public int power;
    public int health;
    public int initiative;
    public int haste;
    public bool[] AOE = new bool[4];

    public string dotName;
    public int dotDamage;
    public int hotRecovery;
    public int dotLength;

    public GameObject dotIcon;

    public EquipmenSlot itemSlot;






}

public enum EquipmenSlot { Weapon, Head, Chest}

public enum OnHitEffect {None,Poison,Bleed,LifeSteal }