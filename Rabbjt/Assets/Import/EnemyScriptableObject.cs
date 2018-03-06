using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Weapon : ScriptableObject
{
    public int power;
    public int health;
    public int initiative;
    public int haste;
    public bool[] AOE = new bool[3];

    public string Effect;


}


