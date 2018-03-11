using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponLibrary : ScriptableObject
{
    [SerializeField] Equipment[] weapons;

    private static WeaponLibrary instance;
    public static WeaponLibrary Instance
    {
         get { return instance ?? (Instance = Resources.Load<WeaponLibrary>("WeaponLibrary")); }
         private set{ instance = value; }
    }

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
