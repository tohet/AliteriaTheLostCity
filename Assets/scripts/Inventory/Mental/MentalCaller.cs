using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MentalCaller
{
	public enum CallMentalType
	{
		TurnStart,
		DamageTaken,
		DamageDealt,
		PathPointPassed
	}

	public static void CallMentals(CallMentalType callMentalType)
	{
		GameObject inventory_holder = GameObject.FindGameObjectWithTag("inventory_holder");
		inventoryObj mental_inventory = inventory_holder.GetComponent<InventoryHolder>().mental_inventory;
		mental_inventory.DoMentalOnInventory(callMentalType);
	}
	public static void CallMentals()
	{
		GameObject inventory_holder = GameObject.FindGameObjectWithTag("inventory_holder");
		inventoryObj mental_inventory = inventory_holder.GetComponent<InventoryHolder>().mental_inventory;
		mental_inventory.DoMentalOnInventory();
	}
}
