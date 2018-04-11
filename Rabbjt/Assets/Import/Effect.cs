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
    private int remainingLenght;

    public  Effect (string name, int damage, int healthrecover, int length, GameObject icon)
    {
        Name = name;
        Damage = damage;
        HealthRecover = healthrecover;
        Length = length;
        Icon = icon;
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
    public virtual void OnApplied()
    {

    }
    public  virtual void OnFallOff()
    {

    }

}
