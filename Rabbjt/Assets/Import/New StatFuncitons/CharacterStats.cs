using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public List<BaseStat> stats = new List<BaseStat>();
    public int health;
    public int power;
    public int armor;
    public int initiative;
    public int haste;
    public bool[] AOE;




	// Use this for initialization
	void Start ()
    {
        stats.Add(new BaseStat(power, "Power", "Your power level"));
        stats.Add(new BaseStat(health, "Health", "Your health level"));
        stats.Add(new BaseStat(initiative, "Initiative", "Your initiative level"));
        stats.Add(new BaseStat(haste, "Haste", "Your haste level"));
        stats.Add(new BaseStat(armor, "Armor", "Your armor level"));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log(stats[0].GetCalculatedValue());
            Debug.Log(stats[1].GetCalculatedValue());
            foreach (bool b in AOE)
            {
                Debug.Log(b);
            }
        }


    }
    public void AddStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat s in statBonuses)
        {
            stats.Find(x => x.StatName == s.StatName).AddBonus(new StatBonus(s.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses)
    {
        foreach (BaseStat s in statBonuses)
        {
            stats.Find(x => x.StatName == s.StatName).RemoveBonus(new StatBonus(s.BaseValue));
        }
    }



}
