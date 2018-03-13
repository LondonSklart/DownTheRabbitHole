using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LootTable : ScriptableObject
{
    [SerializeField] Equipment[] weapons;

    public Equipment GetWeapon(int index)
    {
        return Instantiate(weapons[index]);
    }

    public int GetLibraryLength()
    {
        return weapons.Length;
    }

    public Equipment[] GetAllWeapons()
    {
        return weapons;
    }
}
