using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.SceneManagement;



public class TurnManager : MonoBehaviour
{
    //string = tag
    public GameObject spearmanOBJ;
    public GameObject hinieOBJ;

    public bool round_over = false;
    public int heroes = 0;
    public int enemies = 0;
    public int level_EXP = 0;

    public Dictionary<string, List<Grid_Move>> units = new Dictionary<string, List<Grid_Move>>(); //The main list
    public Queue<string> turnKey = new Queue<string>(); //List of teams
    public Queue<Grid_Move> team_of_unit = new Queue<Grid_Move>(); //List of all units
    public string nowGoes;

    public bool heroes_turn_first;
    bool path_showed = false;

    public GameObject pause_menu;
    public TileColors tiles_color;

    bool hero_EXP_increased = false;

    public delegate void WinBattle();
    public event WinBattle OnBattleWon;

    [System.Serializable]
    public enum TileColors 
    { 
        Desert,
        Caves
    }
    void Start()
    {
        pause_menu = GameObject.FindGameObjectWithTag("pause_menu");

        if (StatHolder.spearman_current_HP > StatHolder.spearman_max_HP)
        {
            StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
            GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
        }
        if (StatHolder.hinie_current_HP > StatHolder.hinie_max_Hp)
        {
            StatHolder.hinie_current_HP = StatHolder.hinie_max_Hp;
            GameObject.FindGameObjectWithTag("hinie_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
            //добавить код изменения здоровья для Хини
        }

        level_EXP = CalculateEXP();
        Debug.Log("This level gives" + level_EXP + "EXP");
    }
    
    void Update()
    {
        CountUnits();
        if (team_of_unit.Count == 0 && !round_over)
        {
            StartCoroutine(InitTeamTurnQueue());
        }


        if (round_over && !path_showed)
        {
            UpdateHealthAfterBattle();

            path_showed = true;
            pause_menu.GetComponent<pause_menu>().CallPathMap();
            //pause_menu.path_point_passed = true;
            //SceneManager.LoadScene("SampleScene");
            ClearTurnManager();
            round_over = false;
        }
    }

    public IEnumerator InitTeamTurnQueue()
    {
        List<Grid_Move> teamList;
        if (heroes_turn_first && turnKey.Peek() == "Player")
        {
            teamList = units[turnKey.Peek()];
        }
        else if (heroes_turn_first)
        {
            turnKey.Enqueue(turnKey.Peek());
            teamList = units[turnKey.Dequeue()];
            teamList = units[turnKey.Peek()];
            heroes_turn_first = false;

        }

        else
            teamList = units[turnKey.Peek()];



        foreach (Grid_Move unit in teamList)
        {
            team_of_unit.Enqueue(unit);
        }
        yield return new WaitForSeconds(.7f);
        StartTurn();
    }

    public void StartTurn()
    {
        if (!round_over) //удалить, как получится убрать моргание path map
        {
            if (units[turnKey.Peek()].Count > 0)
            {
                Grid_Move current_unit = team_of_unit.Peek();
                if (team_of_unit.Count > 0 && current_unit != null/* && ((heroes_turn_first && current_unit.tag != "NPC") || !heroes_turn_first)*/)
                {
                    if (!current_unit.unit_is_dead)
                    {
                        current_unit.moved_last_turn = false;
                        current_unit.BeginTurn();
                        heroes_turn_first = false;
                    }
                }
            }
            else
            {
                units.Remove(turnKey.Peek());
                turnKey.Dequeue();
            }
        }

    }

    public void EndTurn()
    {
        
        if (!round_over) //удалить, как получится убрать моргание path map
        {
            Grid_Move unit = team_of_unit.Dequeue();
            unit.move = unit.max_move;
            unit.EndTurn();

            if (team_of_unit.Count > 0)
            {

                StartTurn();
                //place tutorial window initializaion here

            }
            else
            {
                string team = turnKey.Dequeue();
                turnKey.Enqueue(team);
                InitTeamTurnQueue();
            }
        }
    }


    public void AddUnit(Grid_Move unit)
    {
        List<Grid_Move> list;

        if (!unit.unit_is_dead)
        {
            if (!units.ContainsKey(unit.tag))
            {
                list = new List<Grid_Move>();
                units[unit.tag] = list;

                if (!turnKey.Contains(unit.tag))
                {
                    turnKey.Enqueue(unit.tag);
                }
            }

            else
            {
                list = units[unit.tag];
            }

            list.Add(unit);
        }
    }

    public void RemoveUnit(Grid_Move unit)
    {
        List<Grid_Move> list = units[unit.tag];
        list.Remove(unit);
        if (team_of_unit.Count == 0)
        {
            units.Remove(unit.tag);
            ClearTurnManager();
            //InitTeamTurnQueue();
        }

    }

    public void ClearTurnManager()
    {
        units.Clear();
        turnKey.Clear();
        team_of_unit.Clear();
    }

    public void CountUnits()
    {
        heroes = GameObject.FindGameObjectsWithTag("Player").Length;
        enemies = GameObject.FindGameObjectsWithTag("NPC").Length;

        if (heroes <= 0)
        {
            round_over = true;
            //Destroy(GameObject.FindGameObjectWithTag("pause_menu"));
            SceneManager.LoadScene("intro");
        }

        if (enemies <= 0)
        {
            round_over = true;
            if (!hero_EXP_increased)
            {
                StatHolder.IncreaceEXP(level_EXP);
                hero_EXP_increased = true;
            }
			OnBattleWon?.Invoke();
			//OverWorldManager.IncreaseWins();
		}
    }

    public IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
    }

    public void UpdateHealthAfterBattle()
    {
        if (spearmanOBJ != null)
        {
            Hero_Stats spearman_stats = spearmanOBJ.GetComponent<Hero_Stats>();
            StatHolder.ChangeCurrentHP(spearman_stats.hero_current_HP, 1);
        }

        else
        {
            StatHolder.ChangeCurrentHP(0, 1);
        }

        if (hinieOBJ != null)
        {
            Hero_Stats hinie_stats = hinieOBJ.GetComponent<Hero_Stats>();
            StatHolder.ChangeCurrentHP(StatHolder.spearman_current_HP, hinie_stats.hero_current_HP);
        }
        else
        {
            StatHolder.ChangeCurrentHP(StatHolder.spearman_current_HP, 0);
        }

    }

    int CalculateEXP()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("NPC");
        int total_EXP = 0;
        foreach (GameObject enemy in enemies)
        {
			switch (enemy.GetComponent<NPCMove>().enemyType)
			{
                case NPCMove.EnemyType.BallCrab:
                    total_EXP += 50;
                    break;

                case NPCMove.EnemyType.Cracker:
                    total_EXP += 100;
                    break;

                case NPCMove.EnemyType.Archer:
                    total_EXP += 120;
                    break;

				default:
                    break;
			}
		}

        return System.Convert.ToInt32(total_EXP * StatHolder.exp_multiplier);
    }
}
