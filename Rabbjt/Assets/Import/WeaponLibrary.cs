using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponLibrary : ScriptableObject
{
    [SerializeField] Weapon[] weapons;

    private static WeaponLibrary instance;
    public static WeaponLibrary Instance
    {
         get { return instance ?? (Instance = Resources.Load<WeaponLibrary>("WeaponLibrary")); }
         private set{ instance = value; }
    }

    public Weapon GetWeapon(int index)
    {
        return Instantiate(weapons[index]);
    }

    public Weapon[] GetAllWeapons()
    {
        return weapons;
    }
}
