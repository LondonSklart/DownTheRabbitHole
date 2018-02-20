using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public new string name;
    public int attackDamage;
    public string Effect;
    public bool[] AOE = new bool[3];

}
