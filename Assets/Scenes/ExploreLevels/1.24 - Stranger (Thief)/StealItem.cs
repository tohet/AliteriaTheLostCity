using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealItem : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		inventoryObj player_inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_inventory;

		List<InventorySlot> slots_with_items = new List<InventorySlot>();

		foreach (InventorySlot slot in player_inventory.GetSlots)
		{
			if (slot.item.id != -1 && slot.ammount >= 1)
			{
				slots_with_items.Add(slot);
			}
		}

		int max_value = slots_with_items.Count;

		if (max_value > 0)
		{
			Debug.Log(max_value);

			int rnd_slot_ID = Random.Range(0, max_value - 1);
			InventorySlot slot_to_steal_from = slots_with_items[rnd_slot_ID];

			foreach (InventorySlot slot in player_inventory.GetSlots)
			{
				if (slot == slot_to_steal_from)
				{
					slot.RemoveItem();
					return;
				}
			}

			/*
			Item stolen_item = slots_with_items[rnd_item_ID].item;
			stolen_item.object_mental_act = player_inventory.dataBase.itemsObjects[stolen_item.id].data.object_mental_act;
			player_inventory.RemoveItem(stolen_item);
			*/
		}

	}
}
