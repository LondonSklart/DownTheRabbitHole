using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect
{
    public string Name { get; set; }
    public int Damage { get; set; }
    public int HealthRecover { get; set; }
    public int Length { get; set; }
    private int remainingLenght;

    public  Effect (string name, int damage, int healthrecover, int length)
    {
        Name = name;
        Damage = damage;
        HealthRecover = healthrecover;
        Length = length;
    }

    public void OnEndTurn(GameObject target)
    {
        if (target.GetComponent<PlayerController>() == null)
        {
            target.GetComponent<EnemyController>().TakeDamage(Damage);
        }
        else
        {
            target.GetComponent<PlayerController>().TakeDamage(Damage);
        }
        

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
