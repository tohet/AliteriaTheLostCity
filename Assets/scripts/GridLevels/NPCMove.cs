using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : Grid_Move
{
    public int ap_for_attack = 2;
    public Hero_Stats thisHeroStats;
    public GameObject target;
    List<TileScript> attackableTiles = new List<TileScript>();
    public int range = 1;
    public bool is_attacking = false;
    bool damage_is_dealt = false;
    bool target_in_zone = false;
    public EnemyType enemyType = EnemyType.BallCrab;
    

    public delegate void TrurnStarted();
    public event TrurnStarted OnTurnStart;

    public enum EnemyType
    {
        BallCrab,
        Cracker,
        Archer
    }

	private void Awake()
	{
        if (turnManager == null)
            turnManager = GameObject.Find("GameManager").GetComponent<TurnManager>();
    }
	void Start()
    {
        Initialize();
        if (thisHeroStats == null)
            thisHeroStats = GetComponent<Hero_Stats>();
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        is_attacking = false;

        if ((!turn || thisHeroStats.hero_current_HP <= 0) && !turnManager.round_over)
        {
            return;
        }

        if (!isMoving && !turnManager.round_over)
        {
			FindNearestTarget();

            FindAttackableTiles();


            if (!damage_is_dealt)
            {
                OnTurnStart?.Invoke();
                //если цель на соседней клетке - проверить, есть ли АР на удар

                //если АР есть, ударить 

                //если нет, завершить ход

                //иначе - найти путь к цели

                //FindAttackableTiles();


                CalculatePath();
                FindSelectableTiles();
                actualTargetTile.targetOfMovement = true;



            }

            else
            {
                damage_is_dealt = false;
                //StartCoroutine(WaitAfterAttack());
                //помечено для изменения - должно проверять, есть ли AP

                if (move <= 1)
                {
                    turnManager.EndTurn();
                }
            }
        }
        else if (!turnManager.round_over) //удалить, как получится убрать моргание path map
        {
            Move();
        }
    }

	void CalculatePath()
	{
        //сюда добавить код убегания от игрока (target)
        TileScript targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }



	void FindNearestTarget()
	{
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        // AI code to be put here!!!

        GameObject nearest = null;
        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets)
        {
            float dist = Vector3.Distance(transform.position, obj.transform.position); //Look to use Vector3.SqrMagnitude
            if (dist < distance)
            {
                distance = dist;
                nearest = obj;
            }
            //A-star, quick version of finding something close
        }

        target = nearest;
	}

    public void FindAttackableTiles()
    {
        ComputeAdjacencyList(jumpHeight, null);
        GetCurrentTile();


        Queue<TileScript> process = new Queue<TileScript>();
        process.Enqueue(currentTile);
        currentTile.tileIsVisited = true;

        while (process.Count > 0)
        {
            TileScript t = process.Dequeue();
            attackableTiles.Add(t);
            t.tileIsAttackable = true;

            if (t.distance < range)
            {
                foreach (TileScript tile in t.attackableAdjacencyList)
                {

                    if (!tile.tileIsVisited)
                    {
                        tile.parentTile = t;
                        tile.tileIsVisited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                    RaycastHit hit;
                    if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1, 1 << 8) == target && !damage_is_dealt && move >= ap_for_attack)
                    {

                            t.tileIsAttackable = false;
                            t.tileHasTarget = true;
                            Hero_Stats attackable = target.GetComponent<Hero_Stats>();
                            Vector3 target_vector = new Vector3(attackable.transform.position.x, this.transform.position.y, attackable.transform.position.z);
                            CalculateHeading(target_vector);
                            transform.forward = heading;
                            is_attacking = true;
                            StartCoroutine(Wait());
                            new WaitForSeconds(3);
                            attackable.TakeDamage(thisHeroStats.heroDamage);
                            attackable.healthBar.SetHealth(attackable.hero_current_HP/* -= 20*/);

                        if (attackable.health_display != null)
                            attackable.health_display.ChangeStatDisplay(attackable);

                        target.GetComponent<Grid_Move>().was_attacked_before_turn = true;

                        Debug.Log("NPC: Dealt Damage");
                            damage_is_dealt = true;

                            move -= ap_for_attack;
                            MentalCaller.CallMentals(MentalCaller.CallMentalType.DamageTaken);

                    }
                }
            }
        }
    }

    protected override void Move()
    {
        if (path.Count > 0 && !turnManager.round_over)
        {
            //there are tiles to move to
            TileScript t = path.Peek();
            Vector3 target = t.transform.position;

            //we measure the tile, on which the player is standing
            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)
            {
                bool jump_is_needed = transform.position.y != target.y;
                if (jump_is_needed)
                {
                    Jump(target);
                }
                else
                {
                    CalculateHeading(target);
                    SetHorizontalVelocity();
                }

                //Locomotion
                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                //Tile senter reached
                transform.position = target;
                path.Pop();
                move--;
                //ACTION POINTS DECREASE 
            }
        }

        else if (!turnManager.round_over) //удалить, как получится убрать моргание path map!!!!!
        {
            //we stop the movement

            if (move != 0)
            {
                move++;
            }

            RemoveSelectableTiles();
            isMoving = false;
            //StartCoroutine(WaitAfterAttack());
            //помечено для изменения - должно проверять, есть ли AP
            if (move <= 0)
            {
                turnManager.EndTurn();
            }


            //!!!
            //IT ENDS WHEN THE PLAYER STOPS MOVING
            //NEED TO IMPLY ACTION SYSTEM
            //TO BE CHANGED
            //!!!
        }

    }


    protected IEnumerator Wait()
    {
        Debug.Log("Coroutine works");
        yield return new WaitForSeconds(3);
    }

}
