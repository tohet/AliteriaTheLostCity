using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroGR_Mov_Att : Hero_Grid
{
    public bool tutorial = false;
    bool game_paused = pause_menu.game_paused;
    public int ap_for_attack;
    public bool is_attacking = false;
    public Hero_Stats thisHeroStats;
    List<TileScript> attackableTiles = new List<TileScript>();
    public int range = 3;
    public ItemObject health_potion;
	void Start()
    {
        this.max_move = this.gameObject.GetComponent<Hero_Stats>().heroAP;

        this.move = this.gameObject.GetComponent<Hero_Stats>().heroAP;

        Initialize();
    }

    void Update()
    {
        game_paused = pause_menu.game_paused;
        Debug.DrawRay(transform.position, transform.forward);
        is_attacking = false;

        if (!turn || thisHeroStats.hero_current_HP <= 0)
        {
            return;
        }

        if (!isMoving)
        {
            ap_display.text = this.move.ToString("n0");

            if (!modeIsAttack)
            {
                FindSelectableTiles();

                if (!game_paused)
                CheckMouse();
            }
            else
            {
                FindAttackableTiles();

                if (!game_paused)
                CheckMouseAttack();
            }
        }
        else
        {
            Move();
        }
    }
    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tiles")
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();
                    if (t.selectableTile)
                    {
                        if (tutorial && IsInTutorialTiles(t))
                        {
                            MoveToTile(t);
                            StartCoroutine(Wait());
                            GameObject.FindGameObjectWithTag("tutorial_window").GetComponent<TutorialWindow>().DestroyWindow();
                        }

                        else if (!tutorial)
                        {
                            MoveToTile(t);
                            StartCoroutine(Wait());
                        }
                    }
                }
            }
        }
    }

    void CheckMouseAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            RaycastHit hit_form_NPC;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tiles" && hit.collider.GetComponent<TileScript>().tileHasTarget && this.move >= ap_for_attack)
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();
                    if (t.tileHasTarget)
                    {
                        AttackEntity(t);
                        this.move -= ap_for_attack;

                        if (move <= 0)
                        {
                            turnManager.Invoke("EndTurn", 0.7f);
                        }
                    }
                }

                else if (hit.collider.tag == "NPC" && Physics.Raycast(hit.transform.position, Vector3.down, out hit_form_NPC, 1) && hit_form_NPC.collider.GetComponent<TileScript>().tileHasTarget && this.move >= ap_for_attack)
                {
                    AttackEntity(hit_form_NPC.collider.GetComponent<TileScript>());
                    this.move -= ap_for_attack;


                    if (move <= 0)
                    {
                        turnManager.Invoke("EndTurn", 0.7f);
                    }
                }
            }
        }
    }

    void AttackEntity(TileScript tile)
    {
        Hero_Stats target = FindTargetOfAttack(tile);
        Vector3 target_vector = new Vector3(target.transform.position.x, this.transform.position.y, target.transform.position.z);
        CalculateHeading(target_vector);
        transform.forward = heading;
        is_attacking = true;
        StartCoroutine(Wait());
        MentalCaller.CallMentals(MentalCaller.CallMentalType.DamageDealt);
        target.healthBar.SetHealth(target.hero_current_HP -= thisHeroStats.heroDamage);
        target.gameObject.GetComponent<Grid_Move>().was_attacked_before_turn = true;

        if (tutorial)
        {
            TutTileHolder tut_holder = GameObject.Find("tutorial_holder").GetComponent<TutTileHolder>();
            if (tut_holder.tutorial_tiles[GameObject.FindGameObjectWithTag("battle_tutorial").GetComponent<BattleTutorial>().window_index] == null)
                GameObject.FindGameObjectWithTag("tutorial_window").GetComponent<TutorialWindow>().DestroyWindow();
        }
        //помечено для изменения - должно проверять, есть ли AP
        //turnManager.EndTurn();
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
            RaycastHit hit;
            if (Physics.Raycast(t.transform.position, Vector3.up, out hit, 1))
            {
                t.tileIsAttackable = false;
                t.tileHasTarget = true;
            }

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
                }
            }

        }
    }
    public void RemoveAttackableTiles()
    {
        if (currentTile != null)
        {
            currentTile.entityOnCurrentTile = false;
            currentTile = null;
        }
        foreach (TileScript tile in attackableTiles)
        {
            tile.TilesReset();
        }
        attackableTiles.Clear();
    }

    public Hero_Stats FindTargetOfAttack(TileScript tile)
    {
        RaycastHit hit;
        Hero_Stats target = null;
        if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) && hit.collider.tag == "NPC")
        {
            target = hit.collider.GetComponent<Hero_Stats>();
        }
        return target;
    }

    public void UseHealthPotion()
    {
        if (gameObject.tag == "Player" && turn && !isMoving)
        {
            InventorySlot slot = thisHeroStats.modified_stats_holder.player_inventory.FindItemOnInventory(new Item(health_potion));
            if (slot != null && slot.ammount > 0 && this.move >= 2)
            {
                Debug.Log("Used health potion!");

                this.move -= 2;

                thisHeroStats.hero_current_HP += 20;

                slot.AddAmmount(-1);

                if (slot.ammount <= 0)
                {
                    slot.item.id = -1;
                }

                if (this.move <= 0)
                {
                    //turnManager.EndTurn();
                    turnManager.Invoke("EndTurn", 0.7f);
                }
            }

            else
                Debug.Log("No health potions!");
        }
    }

    public void ChangeGridMode()
    {
        if (gameObject.tag == "Player" && turn && !isMoving)
        {
            if (modeIsAttack)
            {
                RemoveSelectableTiles();
                RemoveAttackableTiles();
                modeIsAttack = false;
            }

            else
            {
                RemoveSelectableTiles();
                RemoveAttackableTiles();
                modeIsAttack = true;
            }
        }
    }

    bool IsInTutorialTiles(TileScript tile)
    {
        TutTileHolder tut_holder = GameObject.Find("tutorial_holder").GetComponent<TutTileHolder>();
        if (tile == tut_holder.tutorial_tiles[GameObject.FindGameObjectWithTag("battle_tutorial").GetComponent<BattleTutorial>().window_index])
            return true;
        else
            return false;
    }
    protected IEnumerator Wait()
    {

        yield return new WaitForSeconds(10f);
       // Debug.Log("Coroutine works");
    }

    public void Hero_EndTurn()
    {
        if (turn && !isMoving)
            turnManager.EndTurn();
    }

    public void DestroyTutorialWindow()
    {
        GameObject.FindGameObjectWithTag("tutorial_window").GetComponent<TutorialWindow>().DestroyWindow();
    }
}
