using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Attack : Grid_Move
{
    public GameObject thisUnit;
    List<TileScript> attackableTiles = new List<TileScript>();
    public int range = 5;
    void Start()
    {
        Initialize(); 
    }

    void Update()
    {
        if (!turn)
        {
            return;
        }

        if (turn && !isMoving)
        {
            FindAttackableTiles();
            CheckMouseAttack();
        }

    }

    void CheckMouseAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Tiles" && hit.collider.GetComponent<TileScript>().entityOnCurrentTile)
                {
                    TileScript t = hit.collider.GetComponent<TileScript>();
                    if (t.tileIsAttackable)
                    {
                        AttackEntity(t);
                    }
                }
            }
        }
    }

    void AttackEntity(TileScript tile)
    {
        GameObject target = FindTargetOfAttack(tile);
        target.GetComponent<Hero_Stats>().hero_current_HP -= 20;
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

            if (t.distance < move)
            {
                foreach (TileScript tile in t.adjacencyList)
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

    public GameObject FindTargetOfAttack(TileScript tile)
    {
        RaycastHit hit;
        GameObject target = null;
        if (Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) && hit.collider.tag == "NPC")
        {
            target = hit.collider.GetComponent<GameObject>();
        }
        return target;
    }
}
