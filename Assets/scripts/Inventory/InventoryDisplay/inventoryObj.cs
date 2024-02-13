using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;
using System;

public enum InterfaceType
{
	Inventory,
	Equipment,
	Mental,
	Chest
}

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class inventoryObj : ScriptableObject
{
	public string save_path;

	public ItemDatabaseObject dataBase;

	public ItemDatabaseObject mentalDataBase;

	public InterfaceType type;

	public inventoryObj usables_inventory;

	public ItemObject item_to_add_from_editor;

	public Inventory container;
	public InventorySlot[] GetSlots { get { return container.slots; } }


	public bool AddItem(Item _item, int _ammount)
	{
		if (EmptySlotCount <= 0)
		{
			return false;
		}

		InventorySlot slot = FindItemOnInventory(_item);

		if (!dataBase.getItem[_item.id].stackable || slot == null)
		{
			
			InventoryHolder inventoryHolder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();

			if (!(dataBase.getItem[_item.id].type == ItemType.Spear && inventoryHolder.player_equipment.container.slots[1].item.id == -1))
			{
				/*
				if (dataBase.getItem[_item.id].type == ItemType.HealthPotion)
				{
					
					foreach (InventorySlot _slot in inventoryHolder.player_inventory.usables_inventory.GetSlots)
					{
						if (_slot.item.id == -1)
						{
							_slot.UpdateSlot(_item, _ammount);
							return true;
						}
					}
				}
				*/
				SetEmptySlot(_item, _ammount);
				return true;
			}


			else if (dataBase.getItem[_item.id].type == ItemType.Spear && inventoryHolder.player_equipment.container.slots[1].item.id == -1)
			{
				foreach (InventorySlot equip_slot in inventoryHolder.player_equipment.GetSlots)
				{
					if (equip_slot.allowed_items[0] == ItemType.Spear && equip_slot.item.id == -1)
					{
						equip_slot.UpdateSlot(_item, _ammount);
						GameObject.FindGameObjectWithTag("Player").GetComponent<Hero>().UpdateEquipment();
						return true;
					}
				}

				SetEmptySlot(_item, _ammount);
				return true;
			}
			/*
			else
			{
				SetEmptySlot(_item, _ammount);

				/*
				inventoryHolder.hp_display.ChangeStatDisplay();
				inventoryHolder.UpdateStatsDisplay();*/
				//return true;
			//}
		}
		slot.AddAmmount(_ammount);
		if (slot.ammount <= 0)
		{
			slot.RemoveItem();
			InventoryHolder inventoryHolder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();
			/*
			inventoryHolder.hp_display.ChangeStatDisplay();
			inventoryHolder.UpdateStatsDisplay();
			*/
		}
		return true;
	}

	public InventorySlot FindItemOnInventory(Item _item)
	{
		for (int i = 0; i < GetSlots.Length; i++)
		{
			if (GetSlots[i].item.id == _item.id)
			{
				return GetSlots[i];
			}
		}

		if (type != InterfaceType.Chest)
		{
			for (int i = 0; i < usables_inventory.GetSlots.Length; i++)
			{
				if (usables_inventory.GetSlots[i].item.id == _item.id)
				{
					return usables_inventory.GetSlots[i];
				}
			}
		}


		return null;
	}

	public int EmptySlotCount
	{
		get
		{
			int counter = 0;

			foreach (InventorySlot slot in GetSlots)
			{
				if (slot.item.id <= -1)
				{
					counter++;
				}
			}
			return counter;
		}
	}
	public InventorySlot SetEmptySlot(Item _item, int _ammount)
	{
		foreach (InventorySlot slot in GetSlots)
		{
			if (slot.item.id <= -1)
			{
				slot.UpdateSlot( _item, _ammount);
				return slot;
			}
		}
		//полный инвентарь (больше нет свободных слтов) - нужно с этим что-то сделать
		return null;
	}

	public void SwapItems(InventorySlot item1, InventorySlot item2)
	{
		if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
		{
			InventorySlot temp = new InventorySlot(item2.item, item2.ammount);
			item2.UpdateSlot(item1.item, item1.ammount);
			item1.UpdateSlot(temp.item, temp.ammount);
		}
	
	}

	public void RemoveItem(Item _item)
	{
		foreach (InventorySlot slot in GetSlots)
		{
			if (slot.item == _item)
			{
				slot.UpdateSlot(null, 0);
			}
		}
	}

	public void DoMentalOnInventory()
	{

		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player.GetComponent<Hero>() != null)
		{
			//playmode is EXPLORE
			for (int i = 0; i < this.GetSlots.Length; i++)
			{
				if (this.GetSlots[i].slot_mental_act != null && this.GetSlots[i].slot_mental_act.IsExplore)
				{
					Debug.Log("Mental used in explore");
					this.GetSlots[i].slot_mental_act.DoMental(true);
				}
			}
		}

		else if (player.GetComponent<HeroGR_Mov_Att>() != null)
		{
			//playmode is BATTLE
			for (int i = 0; i < this.GetSlots.Length; i++)
			{
				if (this.GetSlots[i].slot_mental_act != null && this.GetSlots[i].slot_mental_act.IsBattle)
				{
					Debug.Log("Mental used in battle");
					this.GetSlots[i].slot_mental_act.DoMental(false);
				}
			}
		}

	}

	public void DoMentalOnInventory(MentalCaller.CallMentalType callMentalType)
	{

		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player.GetComponent<Hero>() != null)
		{
			//playmode is EXPLORE
			for (int i = 0; i < this.GetSlots.Length; i++)
			{
				if (this.GetSlots[i].slot_mental_act != null && this.GetSlots[i].slot_mental_act.IsExplore)
				{
					Debug.Log("Mental used in explore");
					this.GetSlots[i].slot_mental_act.DoMental(true, callMentalType);
				}
			}
		}

		else if (player.GetComponent<HeroGR_Mov_Att>() != null)
		{
			//playmode is BATTLE
			for (int i = 0; i < this.GetSlots.Length; i++)
			{
				if (this.GetSlots[i].slot_mental_act != null && this.GetSlots[i].slot_mental_act.IsBattle)
				{
					Debug.Log("Mental used in battle: " + this.GetSlots[i].item.name);
					this.GetSlots[i].slot_mental_act.DoMental(false, callMentalType);
				}
			}
		}
	}

	[ContextMenu("Save")]
	public void Save()
	{
		/*
		string save_data = JsonUtility.ToJson(this, true);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(string.Concat(Application.persistentDataPath, save_path));
		bf.Serialize(file, save_data);
		file.Close();
		*/
		IFormatter formmatter = new BinaryFormatter();
		Stream stream = new FileStream(string.Concat(Application.persistentDataPath, save_path), FileMode.Create, FileAccess.Write);
		formmatter.Serialize(stream, container);
		stream.Close();
	}


	[ContextMenu("Load")]
	public void Load()
	{
		if (File.Exists(string.Concat(Application.persistentDataPath, save_path)))
		{
			/*
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(string.Concat(Application.persistentDataPath, save_path), FileMode.Open);
			JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
			file.Close();
			*/

			IFormatter formatter = new BinaryFormatter();
			Stream stream = new FileStream(string.Concat(Application.persistentDataPath, save_path), FileMode.Open, FileAccess.Read);
			Inventory new_container = (Inventory)formatter.Deserialize(stream);

			for (int i = 0; i < GetSlots.Length; i++)
			{
				GetSlots[i].UpdateSlot(new_container.slots[i].item, new_container.slots[i].ammount);
			}

			stream.Close();
		}
	}

	[ContextMenu("Clear")]
	public void Clear()
	{
		container.Clear();
	}

	[ContextMenu("Set Items Only")]
	public void SetItemsOnly()
	{
		foreach (InventorySlot slot in container.slots)
		{
			slot.allowed_items = new ItemType[] { ItemType.Default, ItemType.HealthPotion, ItemType.Map, ItemType.Key, ItemType.Helmet, ItemType.Chestplate, ItemType.Gloves, ItemType.Boots, ItemType.Spear, ItemType.Bow, ItemType.Trinket };
		}
	}
	/*
	[ContextMenu("Add Item From Editor")]
	public void AddItemFromEditor()
	{
		if (AddItem(item_to_add_from_editor.data, 1))
		{
			Debug.Log("Added an item from editor");
		}
	}
	*/
}

[System.Serializable]
public class Inventory
{
	public InventorySlot[] slots = new InventorySlot[24];
	public void Clear()
	{
		for (int i = 0; i < slots.Length; i++)
		{
			slots[i].RemoveItem();
		}
	}
}


public delegate void SlotUpdated(InventorySlot _slot);


[System.Serializable]
public class InventorySlot
{
	public ItemType[] allowed_items = new ItemType[0];
	[System.NonSerialized]
	public UserIterface parent_interface;
	[System.NonSerialized]
	public GameObject slot_display;
	[System.NonSerialized]
	public SlotUpdated OnAfterUpdate;
	[System.NonSerialized]
	public SlotUpdated OnBeforeUpdate;

	public Item item = new Item();
	//public MentalObject mentalObject;
	//ЧТОБЫ СДЕЛАТЬ СОХРАНЕНИЕ ВОЗМОЖНЫМ НУЖНО ПОМЕТИТЬ МЕНТАЛ АКТ КАК 
	//НО ТОГДА ЮНИТИ СБРАСЫВАЕТ ВСЕ МЕНТАЛ АКТЫ С МЕНТАЛЕЙ
	public MentalAct slot_mental_act;
	public int ammount;

	public ItemObject ItemObject
	{
		get
		{
			if (item.id >= 0)
			{
				return parent_interface.inventory.dataBase.itemsObjects[item.id];
			}

			return null;
		}
	}

	public InventorySlot()
	{
		UpdateSlot(new Item(), 0);
	}

	public InventorySlot(Item _item, int _ammount)
	{
		UpdateSlot(_item, _ammount);
	}

	public void UpdateSlot(Item _item, int _ammount)
	{
		
		if (OnBeforeUpdate != null)
		{
			OnBeforeUpdate.Invoke(this);
		}

		item = _item;
		ammount = _ammount;

		slot_mental_act = _item.object_mental_act;

		if (OnAfterUpdate != null)
		{
			OnAfterUpdate.Invoke(this);
		}
		
	}
	public void RemoveItem()
	{
		UpdateSlot(new Item(), 0);
	}

	public void AddAmmount(int value)
	{
		UpdateSlot(item, ammount += value);
	}

	public bool CanPlaceInSlot(ItemObject _itemObject)
	{
		if (allowed_items.Length <= 0 || _itemObject == null || _itemObject.data.id < 0)
		{
			return true;
		}

		for (int i = 0; i < allowed_items.Length; i++)
		{
			if (_itemObject.type == allowed_items[i])
				return true;
		}

		return false;
	}

}
