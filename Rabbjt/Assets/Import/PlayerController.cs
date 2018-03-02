using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{
    public Weapon weapon;

    float startingHealth = 10;
    private float health;
    private float lifeOnHit;
    float weaponDamage = 2;
    float startingHaste;
    public float haste = 0;
    float buffertimer = 1;
    private bool[] weaponAOE = new bool[] {false,false,false,false };
    TurnManager turnManager;
    TurnController turnController;
    CharacterStats Chstats;
    DamagePrint damagePrint;
    TextController textController;
    private bool attackChoice = false;
    private bool itemChoice = false;
    public Image healthbar;
    [SerializeField]
    List<EnemyController> enemiesList = new List<EnemyController>();

    private void Start()
    {
        weapon = WeaponLibrary.Instance.GetWeapon(1);
        Chstats = gameObject.GetComponent<CharacterStats>();
        turnManager = FindObjectOfType<TurnManager>();
        damagePrint = GetComponentInChildren<DamagePrint>();
        turnController = gameObject.GetComponent<TurnController>();
        LocateEnemies();

        startingHealth = Chstats.stats[1].GetCalculatedValue();
        turnController.initiative = Chstats.stats[2].GetCalculatedValue();
        startingHaste = Chstats.stats[3].GetCalculatedValue();
        weaponAOE = Chstats.AOE;

        health = startingHealth;
        haste = startingHaste;
    }

    private void Update()
    {
        if (haste == 0)
        {
            FindObjectOfType<TurnManager>().EnterQueue(gameObject);
           // haste = startingHaste;
        }
        if (turnController.GetTurn())
        {
            if (attackChoice)
            {

                Attack();
                attackChoice = false;
                turnController.ResetHaste();
                turnController.SetTurn(false);
                turnManager.NewTurn();
                Debug.Log("Hej");
                // turnManager.EndTurn();
                turnManager.DecreaseHaste();



            }
            else if (itemChoice)
            {
                UsePotion();
                itemChoice = false;
                turnManager.EndTurn();
            }
        
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Attack()
    {
        CheckEquipment(weapon.AOE,Chstats.stats[0].GetCalculatedValue(),weapon.Effect);
        LocateEnemies();
        if (weaponAOE[0])
        {
            if (enemiesList.Count > 0)
            {
            enemiesList[0].TakeDamage(weaponDamage);
            }

        }
        if (weaponAOE[1])
        {
            if (enemiesList.Count > 1)
            {
                enemiesList[1].TakeDamage(weaponDamage);
            }
        }
        if (weaponAOE[2])
        {
            if (enemiesList.Count > 2)
            {
                enemiesList[2].TakeDamage(weaponDamage);
            }
        }
        if (weaponAOE[3])
        {
            if (enemiesList.Count > 3)
            {
                enemiesList[3].TakeDamage(weaponDamage);
            }
        }
        if (lifeOnHit > 0)
        {
        GainHealth(lifeOnHit);
        }

    }
    public void TakeDamage(float damage)
    {
        damagePrint.PrintDamage(damage);
        health -= damage;
        healthbar.fillAmount = health / startingHealth;
    }
    public void GainHealth(float damage)
    {
        damagePrint.PrintDamage(damage);
        health += damage;
        healthbar.fillAmount = health / startingHealth;
    }
    public void ChooseAttack()
    {
        attackChoice = true;
    }
    public void ChooseItem()
    {
        itemChoice = true;
    }
    public void UsePotion()
    {
        float potionStrength = 5;
        health += potionStrength;
        if (health > startingHealth)
        {
            health = startingHealth;
        }
        healthbar.fillAmount = health / startingHealth;
    }
    public void LocateEnemies()
    {
        enemiesList.Clear();
        enemiesList.TrimExcess();
        enemiesList.AddRange(FindObjectsOfType<EnemyController>());
    }
    public void CheckEquipment( bool[] AOE, float weaponD, string effect)
    {
        if (AOE.Length >= 1)
        {
            if (AOE[0] == true)
            {
            weaponAOE[0] = true;
            }
        }
        if (AOE.Length >= 2)
        {
            if (AOE[1] == true)
            {
            weaponAOE[1] = true;
            }
        }
        if (AOE.Length >= 3)
        {
            if (AOE[2] == true)
            {
                weaponAOE[2] = true;
            }
        }
        if (AOE.Length >= 4)
        {
            if (AOE[3] == true)
            {
                weaponAOE[3] = true;
            }
        }
        weaponDamage = weaponD;
        switch (effect)
        {
            case "Lifesteal":
                lifeOnHit = 2;
                break;
        }
    }

}
