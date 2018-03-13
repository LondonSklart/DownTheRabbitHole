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
    public EquipmenSlot itemSlot;
    public OnHitEffect onHitEffect;

    public string Effect;


}

public enum EquipmenSlot { Weapon, Head, Chest}

public enum OnHitEffect {None,DoubleStrike,Bleed,LifeSteal }