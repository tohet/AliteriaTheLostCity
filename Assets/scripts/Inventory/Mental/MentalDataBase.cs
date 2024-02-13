using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mental Database", menuName = "Inventory System/Mental/Database")]
public class MentalDataBase : ScriptableObject, ISerializationCallbackReceiver
{
	public MentalObject[] mentalObjects;

	public Dictionary<int, ItemObject> getItem = new Dictionary<int, ItemObject>();

	public void OnAfterDeserialize()
	{
		for (int i = 0; i < mentalObjects.Length; i++)
		{
			mentalObjects[i].data.id = i;
			getItem.Add(i, mentalObjects[i]);
		}
	}

	public void OnBeforeSerialize()
	{
		getItem = new Dictionary<int, ItemObject>();
	}
}