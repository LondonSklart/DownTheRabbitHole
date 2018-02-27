using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy<T> where T : Enemy
{
    public GameObject gameObj;
    public T scriptComponent;

    public Enemy(string name)
    {
        gameObj = new GameObject(name);
        scriptComponent = gameObj.AddComponent<T>();
    }
}

public class Enemy : MonoBehaviour {

    //UI related stuff
    public Sprite enemyUIIcon; //Minimap icon
    public Sprite enemyTexture; //Enemy main sprite
    
    //Generic stats for all enemy types

    float startingHealth;
    float currentHealth;
    float damage;

    float haste;
    float initiative;

    public virtual void OnAwake()
    {

    }

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
