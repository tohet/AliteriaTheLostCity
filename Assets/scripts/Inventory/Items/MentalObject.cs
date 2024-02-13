using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mental Object", menuName = "Inventory System/Items/Mental Object")]
[System.Serializable]
public class MentalObject : ItemObject
{
	public MentalAct mentalAct;
	public delegate void DoMental(bool mode_is_explore);
	public DoMental doMental;

	private void Awake()
	{
		type = ItemType.Mental;
		doMental += mentalAct.DoMental;
	}
}
