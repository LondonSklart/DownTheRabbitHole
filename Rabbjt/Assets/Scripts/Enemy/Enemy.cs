using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy<T> where T : Enemy
{
    public GameObject gameObj;
    public T scriptComponent;
}

public abstract class Enemy : MonoBehaviour {

    public Sprite enemyUIIcon;
    public Sprite enemyTexture;

    string name;

    float startingHealth;
    float currentHealth;
    float damage;

    float haste;
    float initiative;


    public virtual void Attack()
    {

    }
    public virtual void Defend()
    {

    }
    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Death();
        }
    }
    public virtual void OnDamageTaken()
    {

    }
    public virtual void OnDamageDealth()
    {

    }
    public virtual void OnTargeted()
    {

    }
    public virtual void Death()
    {

    }
    public virtual void OnHealingRecieved()
    {

    }
}
