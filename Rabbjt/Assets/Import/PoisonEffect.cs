using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEffect : Effect
{
    private int damage;
    private int healthRecover;
    private int length;
    private int currentLength;

    public PoisonEffect(string _name, int _damage, int _healthrecover, int _length) : base(_name, _damage, _healthrecover, _length)
    {
        damage = _damage;
        healthRecover = _healthrecover;
        currentLength = _length;
    }


    public void OnEndTurn(GameObject target)
    {
        if (target.GetComponent<PlayerController>() == null)
        {
            target.GetComponent<EnemyController>().TakeDamage(damage);
        }
        else
        {
            target.GetComponent<PlayerController>().TakeDamage(damage);
        }
        currentLength--;
    }
    public override void OnApplied()
    {
        currentLength = length;
    }




}
