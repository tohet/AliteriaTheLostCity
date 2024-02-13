using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    public bool tileIsWalkable = true;
    public bool tileIsAttackable = false;
    public bool tileHasTarget = false;
    public bool entityOnCurrentTile = false;
    public bool targetOfMovement = false;
    public bool selectableTile = false;
    // Used to indenify neighbours.
    public List<TileScript> adjacencyList = new List<TileScript>();
    public List<TileScript> attackableAdjacencyList = new List<TileScript>();
    //BFS
    public bool tileIsVisited = false;
    public TileScript parentTile = null;
    public int distance = 0;
    //A* (A-Star) - looks for best path
    public float f_cost = 0;
    public float g_costParentToTile = 0;
    public float h_costProcessTileToDestiny = 0;
    Color base_tile_color;
    Color sandFloor = new Color(1f, 0.8f, 0.4f); //для пустыни
    Color caveFloor = new Color(0.4f, 0.4f, 0.4f); //для пещер

    TurnManager turnManager;
    public void TilesReset()
    {

        tileIsWalkable = true;
        tileIsAttackable = false;
        tileHasTarget = false;
        entityOnCurrentTile = false;
        targetOfMovement = false;
        selectableTile = false;
        // Used to indenify neighbours.
        adjacencyList.Clear();
        attackableAdjacencyList.Clear();
        //BFS
        tileIsVisited = false;
        parentTile = null;
        distance = 0;
        f_cost = g_costParentToTile = h_costProcessTileToDestiny = 0;
    }

    public void CheckTile(Vector3 direction, float jumpHeight, TileScript target)
    {
        Vector3 halfExtents = new Vector3(0.25f, (1 + jumpHeight) / 2f, 0.25f);

        Collider[] colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

        foreach (Collider item in colliders)
        {
            TileScript tile = item.GetComponent<TileScript>();
            if (tile != null && tile.tileIsWalkable)
            {
                RaycastHit hit;
                if (!Physics.Raycast(tile.transform.position, Vector3.up, out hit, 1) || (tile == target))
                {
                    adjacencyList.Add(tile);
                    
                }

                attackableAdjacencyList.Add(tile);
            }
        }
    }

    public void FindNeighbours(float jumpHeight, TileScript target)
    {
        TilesReset();
        CheckTile(Vector3.forward, jumpHeight, target);
        CheckTile(-Vector3.forward, jumpHeight, target);
        CheckTile(Vector3.right, jumpHeight, target);
        CheckTile(-Vector3.right, jumpHeight, target);
    }

    void Start()
    {
        turnManager = GameObject.FindGameObjectWithTag("turn_manager").GetComponent<TurnManager>();

		switch (turnManager.tiles_color)
		{
            case TurnManager.TileColors.Caves:
                base_tile_color = caveFloor;
                break;

            case TurnManager.TileColors.Desert:
                base_tile_color = sandFloor;
                break;
			default:
                base_tile_color = Color.white;
                break;
		}
	}

    // Update is called once per frame
    void Update()
    {
        if (entityOnCurrentTile)
        {
            GetComponent<Renderer>().material.color = Color.gray;
        }

        else if (targetOfMovement)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }

        else if (selectableTile)
        {
            GetComponent<Renderer>().material.color = Color.yellow;
        }

        else if (tileIsAttackable)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }

        else if (tileHasTarget)
        {
            GetComponent<Renderer>().material.color = Color.magenta;
        }

        else
        {
            GetComponent<Renderer>().material.color = base_tile_color;
        }



}
}
