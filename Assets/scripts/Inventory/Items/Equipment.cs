using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment Object", menuName = "Inventory System/Items/Equipment")]
public class Equipment : ItemObject
{
	public int attack_bonus;
	public int defence_bonus;
	public void Awake()
	{
		//type = ItemType.Chestplate;
	}
}
