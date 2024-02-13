using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeMental : MonoBehaviour
{
	public MChFiller mCh_filler;

	public int corelated_mental_slot_ID;

	public bool is_skipmental = false;
	public void TakeMentalFromSlot()
	{
		if (!is_skipmental)
		{
			inventoryObj mental_inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().mental_inventory;
			mental_inventory.AddItem(mCh_filler.choose_slots.GetSlots[corelated_mental_slot_ID].item, 1);
			mCh_filler.TakeMental(mCh_filler.choose_slots.GetSlots[corelated_mental_slot_ID].item.id);
		}

		else
		{
			StatHolder.IncreaceEXP(StatHolder.exp_to_level_up / 3);
			corelated_mental_slot_ID = -1;
			mCh_filler.TakeMental(-1);
		}


	}
}
