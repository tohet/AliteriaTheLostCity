using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDatabaseObject : ScriptableObject, ISerializationCallbackReceiver
{
	public ItemObject[] itemsObjects;

	public Dictionary<int, ItemObject> getItem = new Dictionary<int, ItemObject>();

	public void OnAfterDeserialize()
	{
		for (int i = 0; i < itemsObjects.Length; i++)
		{
			itemsObjects[i].data.id = i;
			getItem.Add(i, itemsObjects[i]);
		}
	}

	public void OnBeforeSerialize()
	{
		getItem = new Dictionary<int, ItemObject>();
	}
}
