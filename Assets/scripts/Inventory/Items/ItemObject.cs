using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
	Default,

	HealthPotion,
	Map,
	Key,

	Helmet,
	Chestplate,
	Gloves,
	Boots,

	Spear,
	Bow,

	Trinket,

	Mental
}

public enum Attribute_Type
{
	HP,
	AP,
	DEF,
	DMG,
	INT,
	JUMP
}
[System.Serializable]
public abstract class ItemObject : ScriptableObject
{
	//holds display for the item

	public Sprite ui_display;

	public bool stackable;
	public bool is_used_in_battle;
	public bool is_used_in_explore;

	public ItemType type;
	[TextArea (15, 20)]
	public string discription;

	public Item data = new Item();

	public int heal_value;
	public int resote_AP_value;
}

[System.Serializable]
public class Item
{
	public string name;
	public int id = -1;
	public ItemBuff[] buffs;
	//public MentalObject itemMentalObject;
	//ЧТОБЫ СДЕЛАТЬ СОХРАНЕНИЕ ВОЗМОЖНЫМ НУЖНО ПОМЕТИТЬ МЕНТАЛ АКТ КАК [System.NonSerializable] 
	//НО ТОГДА ЮНИТИ СБРАСЫВАЕТ ВСЕ МЕНТАЛ АКТЫ С МЕНТАЛЕЙ
	public MentalAct object_mental_act;
	public Item()
	{
		name = "";
		id = -1;
		//itemMentalObject = null;
		object_mental_act = null;
	}

	public Item(ItemObject item)
	{
		name = item.name; 
		id = item.data.id;
		buffs = new ItemBuff[item.data.buffs.Length];
		for (int i = 0; i < buffs.Length; i++)
		{
			buffs[i] = new ItemBuff(item.data.buffs[i].value)
			{
				attribute = item.data.buffs[i].attribute
			};
		}
	}

	public void DoMentalFromItem()
	{

	}

	[System.Serializable]
	public class ItemBuff : IModifiers
	{
		public Attribute_Type attribute;
		public int value;

		public ItemBuff(int _value)
		{
			value = _value;
		}

		void IModifiers.AddValue(ref int baseValue)
		{
			baseValue += value;
		}
	}
}