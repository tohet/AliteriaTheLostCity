using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartUI : MonoBehaviour
{
    void Start()
    {
        GameObject game_UI;
        game_UI = GameObject.FindGameObjectWithTag("pause_menu");
        if (game_UI != null)
        {
            
            Path_mngr.pause_menu_instantiated = false;
            Path_mngr.inventory_holder_instantiated = false;
           // game_UI.ResetDay();
            StatHolder.ResetStats();
            StoryProgression.ResetStoryProgression();
            GameObject inv_holder = GameObject.FindGameObjectWithTag("inventory_holder");
            InventoryHolder inv = inv_holder.GetComponent<InventoryHolder>();


            for (int i = 0; i < inv.player_equipment.GetSlots.Length; i++)
            {
                inv.player_equipment.GetSlots[i].OnBeforeUpdate = null;
                inv.player_equipment.GetSlots[i].OnAfterUpdate = null;
            }

            for (int i = 0; i < inv.player_inventory.GetSlots.Length; i++)
            {
                inv.player_inventory.GetSlots[i].OnBeforeUpdate = null;
                inv.player_inventory.GetSlots[i].OnAfterUpdate = null;
            }

            for (int i = 0; i < inv.mental_inventory.GetSlots.Length; i++)
            {
                inv.mental_inventory.GetSlots[i].OnBeforeUpdate = null;
                inv.mental_inventory.GetSlots[i].OnAfterUpdate = null;
            }

            for (int i = 0; i < inv.player_inventory.usables_inventory.GetSlots.Length; i++)
            {
                inv.player_inventory.usables_inventory.GetSlots[i].OnBeforeUpdate = null;
                inv.player_inventory.usables_inventory.GetSlots[i].OnAfterUpdate = null;
            }
            inv.ResetInventory();
            Destroy(inv_holder);
            Destroy(game_UI);
        }
    }
}
