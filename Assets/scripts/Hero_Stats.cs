using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_Stats : MonoBehaviour
{
    public InventoryHolder modified_stats_holder;
    public TurnManager turnManager;
    public GameObject hero;
    public int hero_max_HP;
    public int hero_current_HP;
    public int heroAP;
    public int heroDamage = 50;
    public Stat_Display health_display;
    public int hero_DEF = 0;
    public bool spearman;
    public bool hinie;

    public HealthBar healthBar;
    void Awake()
    {
        if (turnManager == null)
            turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
        modified_stats_holder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();

        if (hinie)
        {
            /*
            this.hero_max_HP = StatHolder.hinie_MAX_HP;
            healthBar.SetMaxHealth(StatHolder.hinie_MAX_HP);
            this.hero_current_HP = StatHolder.hinie_current_HP;
            healthBar.SetHealth(StatHolder.hinie_current_HP);
            this.heroAP = StatHolder.hinie_AP;
            this.heroDamage = StatHolder.hinie_DMG;
            */


            foreach (Attribute stat in modified_stats_holder.hinie_attributes)
            {
                switch (stat.type)
                {
                    case Attribute_Type.HP:
                        hero_max_HP = stat.value.ModifiedValue;
                        healthBar.SetMaxHealth(hero_max_HP);
                        break;
                    case Attribute_Type.AP:
                        heroAP = stat.value.ModifiedValue;
                        if (StatHolder.hinie_hit_in_knee)
                            heroAP -= 2;
                        break;
                    case Attribute_Type.DEF:
                        hero_DEF = stat.value.ModifiedValue + StatHolder.hinie_armor_bonus;
                        break;
                    case Attribute_Type.DMG:
                        heroDamage = stat.value.ModifiedValue;
                        break;
                    case Attribute_Type.INT:
                        break;
                    case Attribute_Type.JUMP:
                        break;
                    default:
                        break;
                }

                hero_current_HP = StatHolder.hinie_current_HP;
                healthBar.SetHealth(StatHolder.hinie_current_HP);
                health_display = GameObject.FindGameObjectWithTag("hinie_health_display").GetComponent<Stat_Display>();

            }
            if (hero_current_HP > hero_max_HP)
            {
                hero_current_HP = hero_max_HP;
            }

            Debug.Log("Initialized Hinie stats");
        }

        if (spearman)
        {
            
            foreach (Attribute stat in modified_stats_holder.spearman_attributes)
            {
				switch (stat.type)
				{
					case Attribute_Type.HP:
                        this.hero_max_HP = stat.value.ModifiedValue;
                        healthBar.SetMaxHealth(this.hero_max_HP);
						break;
					case Attribute_Type.AP:
                        this.heroAP = stat.value.ModifiedValue;
                        if (StatHolder.spearman_hit_in_knee)
                            this.heroAP -= 2;
                        break;
					case Attribute_Type.DEF:
                        this.hero_DEF = stat.value.ModifiedValue + StatHolder.spearman_armor_bonus;
						break;
					case Attribute_Type.DMG:
                        this.heroDamage = stat.value.ModifiedValue;
						break;
					case Attribute_Type.INT:
						break;
					case Attribute_Type.JUMP:
						break;
					default:
						break;
				}

                this.hero_current_HP = StatHolder.spearman_current_HP;
                healthBar.SetHealth(StatHolder.spearman_current_HP);
                health_display = GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>();

            }
            if (this.hero_current_HP > this.hero_max_HP)
            {
                this.hero_current_HP = this.hero_max_HP;
            }
            Debug.Log("Initialized Spearman stats");
        }

        //healthBar.SetMaxHealth(hero_max_HP);
    }

    public void TakeDamage(int damage)
    {
        if (hero_DEF > 0)
        {
            int dealt_damage = damage - hero_DEF;

            if (spearman)
            {
                StatHolder.spearman_armor_bonus -= damage;
                if (StatHolder.spearman_armor_bonus < 0)
                    StatHolder.spearman_armor_bonus = 0;
            }

            else if (hinie)
            {
                StatHolder.hinie_armor_bonus -= damage;
                if (StatHolder.hinie_armor_bonus < 0)
                    StatHolder.hinie_armor_bonus = 0;
            }

            if (dealt_damage < 0)
                dealt_damage = 0;
            hero_DEF -= damage;
            if (hero_DEF < 0)
                hero_DEF = 0;
            GameObject[] armor_displays = GameObject.FindGameObjectsWithTag("armor_display");
            foreach (GameObject armor_display in armor_displays)
            {
                if (spearman && armor_display.GetComponent<ArmorDisplay>().spearman)
                    armor_display.GetComponent<ArmorDisplay>().armor_label.text = hero_DEF.ToString();
                else if (hinie && !armor_display.GetComponent<ArmorDisplay>().spearman)
                    armor_display.GetComponent<ArmorDisplay>().armor_label.text = hero_DEF.ToString();
            }
            hero_current_HP -= dealt_damage;
        }

        else
            hero_current_HP -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (this.gameObject.tag == "NPC")
            {
                hero_current_HP -= 50;
                healthBar.SetHealth(hero_current_HP);
            }

        }
        */
        if (hero_current_HP <= 0 && hero.GetComponent<Grid_Move>().turn == true)
        {
            Debug.Log("Hero is dead!");
            hero.GetComponent<Grid_Move>().turn = false;
            hero.GetComponent<Grid_Move>().unit_is_dead = true;
            turnManager.RemoveUnit(hero.GetComponent<Grid_Move>());
            turnManager.EndTurn();
            Destroy(hero);
        }
    }


}
