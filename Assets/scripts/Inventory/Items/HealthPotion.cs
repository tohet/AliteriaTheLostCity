using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New HealthPotion Object", menuName = "Inventory System/Items/Health Potion")]
public class HealthPotion : ItemObject
{

	public void Awake()
	{
		type = ItemType.HealthPotion;
	}
}
