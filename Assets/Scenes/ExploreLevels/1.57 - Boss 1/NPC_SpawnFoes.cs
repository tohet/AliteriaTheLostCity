using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SpawnFoes : MonoBehaviour
{
    public NPCMove npc_to_spawn;
    public int ammount_of_spawned_NPCs;
    public int turns_to_recharge;
    int spawned_npcs = 0;

    NPCMove this_NPC;
    int turns_since_last_spawn = 10;
    void Awake()
    {
        this_NPC = GetComponent<NPCMove>();
        this_NPC.OnTurnStart += SpawnNPCs;
        Debug.Log("Initiazed NPC spawner");
    }

    public void SpawnNPCs()
    {
        if (turns_since_last_spawn >= turns_to_recharge)
        {
            //spawns NPCs
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tiles");
            //List<int> tile_to_spawn_IDs = new List<int>(ammount_of_spawned_NPCs);

            int[] ties_IDs = new int[ammount_of_spawned_NPCs];

			for (int i = 0; i < ties_IDs.Length; i++)
			{
                int to_add_rnd_id;
                if (i != 0)
                {
                    do
                    {
                        to_add_rnd_id = UnityEngine.Random.Range(0, tiles.Length);
                    } while (!HasValue(ties_IDs, to_add_rnd_id) && !TileIsOccupied(tiles, to_add_rnd_id));
                    ties_IDs[i] = to_add_rnd_id;
                }
                else
                {
					do
					{
                        to_add_rnd_id = UnityEngine.Random.Range(0, tiles.Length);
                    } while (!TileIsOccupied(tiles, to_add_rnd_id));
                    ties_IDs[i] = to_add_rnd_id;
                }
			}

            foreach (int spawn_tile_ID in ties_IDs)
            {
                
                Vector3 spawn_coordinates = new Vector3(tiles[spawn_tile_ID].transform.position.x, 1.4f, tiles[spawn_tile_ID].transform.position.z);
                NPCMove spawned_NPC = Instantiate(npc_to_spawn, spawn_coordinates, new Quaternion());
                spawned_NPC.thisHeroStats.hero_current_HP = spawned_NPC.thisHeroStats.hero_max_HP;
                spawned_NPC.max_move = spawned_NPC.move;
                //tiles[spawn_tile_ID].transform.position = new Vector3(tiles[spawn_tile_ID].transform.position.x, 1, tiles[spawn_tile_ID].transform.position.z); //поднимает тайлы на 1 вверх (ЗЕМЛЕТРЯСЕНИЕ)
                //tiles[spawn_tile_ID].GetComponent<TileScript>().tileHasTarget = true;
                Debug.Log("Picked tile with ID " + spawn_tile_ID + " Coordinates: " + tiles[spawn_tile_ID].transform.position);
            }

            Debug.Log("Spawned NPCs");

            this_NPC.move = 0;

            turns_since_last_spawn = 0;
        }

        else
            turns_since_last_spawn++;
    }

    bool TileIsOccupied(GameObject[] tiles, int tile_index)
    {
        TileScript tile = tiles[tile_index].GetComponent<TileScript>();
        if (tile.tileIsWalkable && !tile.entityOnCurrentTile && !tile.tileHasTarget && !tile.tileIsVisited)
            return false;
        return true;
    }

    bool HasValue(int[]int_array, int value)
    {
        foreach (int checked_value in int_array)
        {
            if (checked_value == value)
                return false;
        }
        return true;
    }
}
