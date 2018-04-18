using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour

{

    int currentRoom;

    public float startingHealth = 10;
    public float armor = 1;
    public float bonusDamage;
    public float fragile = 0;
    public float health;
    private float lifeOnHit;
    float startingHaste;
    public float haste = 0;
    private bool[] weaponAOE = new bool[] {false,false,false,false };
    TurnManager turnManager;
    TurnController turnController;
    CharacterStats Chstats;
    DamagePrint damagePrint;
    TextController textController;
    WorldBuilder worldBuilder;
    Animation animator;

    public GameObject potionBelt;
    public GameObject invetoryUI;
    public GameObject IconParent;

    Vector3 originPosition;

    PlayerWeaponController weaponController;
    private bool attackChoice = false;
    private bool itemChoice = false;
    public Image healthbar;
    [SerializeField]
    List<EnemyController> enemiesList = new List<EnemyController>();

    //public List<Effect> effects = new List<Effect>();
    List<GameObject> templist = new List<GameObject>();
    public List<Effect> afflictedList = new List<Effect>();


    public int CurrentRoom
    {
        get
        {
            return currentRoom;
        }

        set
        {
            currentRoom = value;
        }
    }

    private void Start()
    {
        //weapon = WeaponLibrary.Instance.GetWeapon(1);
        weaponController = gameObject.GetComponent<PlayerWeaponController>();
        Chstats = gameObject.GetComponent<CharacterStats>();
        turnManager = FindObjectOfType<TurnManager>();
        damagePrint = GetComponentInChildren<DamagePrint>();
        turnController = gameObject.GetComponent<TurnController>();
        animator = gameObject.GetComponent<Animation>();

        LocateEnemies();
        originPosition = gameObject.transform.position;

        startingHealth = Chstats.stats[1].GetCalculatedValue();
        turnController.initiative = Chstats.stats[2].GetCalculatedValue();
        startingHaste = Chstats.stats[3].GetCalculatedValue();
        weaponAOE = Chstats.AOE;
        worldBuilder = FindObjectOfType<WorldBuilder>();
        health = startingHealth;
        haste = startingHaste;
    }

    private void Update()
    {
        if(turnManager.GetFighting() == false) PlayerNavigation();
       

        if (haste == 0)
        {
            //FindObjectOfType<TurnManager>().EnterQueue(gameObject);
           // haste = startingHaste;
        }
        if (turnController.GetTurn())
        {
            if (attackChoice)
            {

                Attack(Chstats.stats[0].GetCalculatedValue());
                attackChoice = false;
                turnController.ResetHaste();
                turnController.SetTurn(false);
                turnManager.NewTurn();
                // turnManager.EndTurn();
                turnManager.DecreaseHaste();

                float damageCache = 0;

                foreach (Effect e in afflictedList)
                {
                    if (e.Length > 0)
                    {

                        damageCache += e.OnEndTurn();

                        e.Length--;
                    }

                }
                if (afflictedList.Count > 0 && damageCache > 0)
                {
                    TakeDamage(damageCache, false);
                }

                for (int i = 0; i < afflictedList.Count; i++)
                {
                    if (afflictedList[i].Length <= 0)
                    {
                        Debug.Log(afflictedList[i].Name + " has fallen off from " + gameObject.name);

                        if (afflictedList[i].Name == "Strength")
                        {
                            bonusDamage -= 10;
                        }
                        if (afflictedList[i].Name == "Haste")
                        {
                            turnController.Startinghaste += 2;
                            turnController.haste += 2;

                        }

                        afflictedList[i].OnFallOff(gameObject);

                        for (int j = 0; j < templist.Count; j++)
                        {
                           

                                if (afflictedList[i].Icon.GetComponent<Image>().sprite.name == templist[j].GetComponent<Image>().sprite.name)
                                {
                                    Destroy(templist[j].gameObject);
                                    templist.Remove(templist[j]);
                                }
                            
                        }


                        afflictedList.Remove(afflictedList[i]);


                    }
                }
            }

            turnController.CheckEndOfFight();

        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void PlayerNavigation()
    {
        RoomManager rm = FindObjectOfType<RoomManager>();
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W pressed");

            StartCoroutine(Move("Up"));
            
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            StartCoroutine(Move("Left"));

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Move("Down"));

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(Move("Right"));

        }
        if(Input.GetKeyDown(KeyCode.F) && currentRoom == worldBuilder.EndRoom){
            worldBuilder.CurrentFloor++;
            worldBuilder.GenerateFloor();
        }
    }
    public void Attack(float damage)
    {
       // CheckEquipment(Chstats.AOE,Chstats.stats[0].GetCalculatedValue());
        LocateEnemies();
        animator.Play("PlayerAttackAnimation");
        DealDamage(damage + bonusDamage);


        turnManager.CheckIfVictorious();
    }
    public void TakeDamage(float damage, bool affectedByArmor)
    {

        if (affectedByArmor == true)
        {
            damage -= armor;

            Debug.Log("Damage affected by armor " + damage);
            if (damage <= 0)
            {
                damage = 1;
            }
        }

        damagePrint.PrintDamage(damage.ToString());
        health -= damage;
        healthbar.fillAmount = health / startingHealth;

    }
    public void DealDamage(float damage)
    {
        if (Chstats.AOE[0])
        {
            if (enemiesList.Count > 0)
            {
                enemiesList[0].TakeDamage(damage,true);
                EffectCheck(enemiesList[0]);

            }

        }
        if (Chstats.AOE[1])
        {
            if (enemiesList.Count > 1)
            {
                enemiesList[1].TakeDamage(damage,true);
                EffectCheck(enemiesList[1]);
            }
        }
        if (Chstats.AOE[2])
        {
            if (enemiesList.Count > 2)
            {
                enemiesList[2].TakeDamage(damage,true);
                EffectCheck(enemiesList[2]);
            }
        }
        if (Chstats.AOE[3])
        {
            if (enemiesList.Count > 3)
            {
                enemiesList[3].TakeDamage(damage,true);
                EffectCheck(enemiesList[3]);
            }
        }

    }
    public void EffectCheck(EnemyController target)
    {
        foreach (Item item in weaponController.EquipedItems)
        {
            if (item.OnHitEffect.Name.Length > 0)
            {
                Debug.Log("Afflicting: " + target + " with: " + item.OnHitEffect.Name + "From weapon: " + item.Name);
                target.Afflicted(item.OnHitEffect);

            }
        }
    }
    public void GainHealth(float damage)
    {
        Debug.Log("Health before heal" + health);
        damagePrint.PrintDamage(damage.ToString());
        health += damage;
        Debug.Log("Health after heal" + health);

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
    public void CheckEquipment( bool[] AOE, float weaponD)
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

    }
    public void OpenInventory()
    {
        invetoryUI.SetActive(!invetoryUI.activeSelf);
    }
    public void OpenPotionBelt()
    {
        potionBelt.SetActive(!potionBelt.activeSelf);

    }
    public void Afflicted(Effect effect)
    {
        bool check = false;
        foreach (Effect e in afflictedList)
        {
            if (e.Name == effect.Name)
            {
                e.Length = effect.Length;
                Debug.Log("Refreshing dot");
                check = true;
            }
        }
        if (check == true)
        {

        }
        else
        {
            Debug.Log(gameObject.name + "Recieved dot: " + effect.Name);
            afflictedList.Add(new Effect(effect.Name, effect.Damage, effect.HealthRecover, effect.Length, effect.Icon, effect.ArmorShred, effect.FragileLevel));
            Debug.Log("Armor before shred" + armor);
            effect.OnApplied(gameObject);
            Debug.Log("Armor after shred" + armor);

            if (effect.Icon != null)
            {
                templist.Add(Instantiate(effect.Icon, IconParent.transform));

            }
            StartCoroutine(TimedPopUp(0.5f, afflictedList));

        }
    }

    IEnumerator TimedPopUp(float time, List<Effect> dotList)
    {


        for (int i = 0; i < dotList.Count; i++)
        {
            yield return new WaitForSeconds(time);
            if (dotList[i].GetMentioned()== false)
            {
                damagePrint.PrintDamage(dotList[i].Name);
                dotList[i].Mentioned();
            }
        }



    }
    IEnumerator Move(string input)
    {
        RoomManager rm = FindObjectOfType<RoomManager>();

        switch (input)
        {
            case "Up":
                animator.Play("PlayerRoomTransitionUp");
                yield return new WaitForSeconds(animator.GetClip("PlayerRoomTransitionUp").length);

                rm.OnRoomLoad(currentRoom = WorldBuilder.Calc_N(currentRoom, true));
                yield return new WaitForSeconds(0.1f);

                gameObject.transform.position = originPosition;
                Debug.Log(gameObject.transform.position == originPosition);
                break;
            case "Right":
                animator.Play("PlayerRoomTransitionRight");
                yield return new WaitForSeconds(animator.GetClip("PlayerRoomTransitionRight").length);

                rm.OnRoomLoad(currentRoom = WorldBuilder.Calc_E(currentRoom, true));
                yield return new WaitForSeconds(0.1f);

                gameObject.transform.position = originPosition;
                Debug.Log(gameObject.transform.position == originPosition);
                break;
            case "Left":
                animator.Play("PlayerRoomTransitionLeft");
                yield return new WaitForSeconds(animator.GetClip("PlayerRoomTransitionLeft").length);

                rm.OnRoomLoad(currentRoom = WorldBuilder.Calc_W(currentRoom, true));
                yield return new WaitForSeconds(0.1f);

                gameObject.transform.position = originPosition;
                Debug.Log(gameObject.transform.position == originPosition);
                break;
            case "Down":
                animator.Play("PlayerRoomTransitionDown");
                yield return new WaitForSeconds(animator.GetClip("PlayerRoomTransitionDown").length);

                rm.OnRoomLoad(currentRoom = WorldBuilder.Calc_S(currentRoom, true));
                yield return new WaitForSeconds(0.1f);

                gameObject.transform.position = originPosition;
                Debug.Log(gameObject.transform.position == originPosition);
                break;
        }
    }

    
}
