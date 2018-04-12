using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int HealthRecover { get; set; }
    public int Length { get; set; }
    public GameObject Icon { get; set; }
    public int ArmorShred { get; set; }
    public int FragileLevel { get; set; }
    private int remainingLenght;

    public  Effect (string name, int damage, int healthrecover, int length, GameObject icon,int armorshred,int fragilelevel)
    {
        Name = name;
        Damage = damage;
        HealthRecover = healthrecover;
        Length = length;
        Icon = icon;
        ArmorShred = armorshred;
        FragileLevel = fragilelevel;
    }

    public float OnEndTurn()
    {
        //if (target.GetComponent<PlayerController>() == null)
        //{
        //    target.GetComponent<EnemyController>().TakeDamage(Damage);
        //}
        //else
        //{
        //    target.GetComponent<PlayerController>().TakeDamage(Damage);
        //}



        return Damage;
    }
    public virtual void TurnTick()
    {

    }
    public void OnApplied(GameObject target)
    {
        if (ArmorShred > 0)
        {


            Debug.Log("Shredding armor");
            if (target.GetComponent<PlayerController>() == null)
            {
                target.GetComponent<EnemyController>().armor -= ArmorShred;
                target.GetComponent<EnemyController>().fragile += FragileLevel;

            }
            else
            {
                target.GetComponent<EnemyController>().armor -= ArmorShred;
                target.GetComponent<EnemyController>().fragile += FragileLevel;

            }
        }
    }
    public void OnFallOff(GameObject target)
    {
        Debug.Log("Restoring armor");
        if (ArmorShred > 0)
        {


            if (target.GetComponent<PlayerController>() == null)
            {
                target.GetComponent<EnemyController>().armor += ArmorShred;
                target.GetComponent<EnemyController>().fragile -= FragileLevel;

            }
            else
            {
                target.GetComponent<EnemyController>().armor += ArmorShred;
                target.GetComponent<EnemyController>().fragile -= FragileLevel;

            }
        }
    }

}
