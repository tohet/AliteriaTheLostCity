using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{

	public MouseItem mouse_item = new MouseItem();

	public Stat_Display hp_display;
	public Stat_Display hinie_health_display;
	// сохраняет инвентарь между сценами
	public StatDataBase spearman_base_stats;
	public StatDataBase hinie_base_stats;
	public inventoryObj player_inventory;
	public inventoryObj player_equipment;
	public inventoryObj mental_inventory;
	public inventoryObj mental_choose;

	public DynamicInterface inventory_interface;
	public StaticInterface equipment_interface;
	public MentalInterface mentals_interface;
	public StaticInterface usables_interface;

	public GameObject hinie_stats_display;

	public Attribute[] spearman_attributes;
	public Attribute[] hinie_attributes;



	public void Awake()
	{
		if (Path_mngr.inventory_holder_instantiated)
		{
			GameObject.Destroy(this.gameObject);
		}

		else
		{
			inventory_interface.InitializeSlots();
			equipment_interface.InitializeSlots();
			mentals_interface.InitializeSlots();
			usables_interface.InitializeSlots();

			GameObject.DontDestroyOnLoad(this.gameObject);
			Path_mngr.inventory_holder_instantiated = true;


			for (int i = 0; i < player_equipment.GetSlots.Length; i++)
			{
				player_equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
				player_equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
			}

			for (int i = 0; i < spearman_attributes.Length; i++)
			{
				spearman_attributes[i].SetParent(this);

				switch (spearman_attributes[i].type)
				{
					case Attribute_Type.HP:
						spearman_attributes[i].value.BaseValue = spearman_base_stats.hero_HP.value.BaseValue;
						Debug.Log("Attribute HP has index " + i);
						break;
					case Attribute_Type.AP:
						spearman_attributes[i].value.BaseValue = spearman_base_stats.hero_AP.value.BaseValue;
						Debug.Log("Attribute AP has index " + i);
						break;
					case Attribute_Type.DEF:
						spearman_attributes[i].value.BaseValue = spearman_base_stats.hero_DEF.value.BaseValue;
						Debug.Log("Attribute DEF has index " + i);
						break;
					case Attribute_Type.DMG:
						spearman_attributes[i].value.BaseValue = spearman_base_stats.hero_DMG.value.BaseValue;
						Debug.Log("Attribute DMG has index " + i);
						break;
					case Attribute_Type.INT:
						spearman_attributes[i].value.BaseValue = spearman_base_stats.hero_INT.value.BaseValue;
						Debug.Log("Attribute INT has index " + i);
						break;
					case Attribute_Type.JUMP:
						spearman_attributes[i].value.BaseValue = spearman_base_stats.hero_JUMP.value.BaseValue;
						break;
					default:
						break;
				}
			}

			for (int i = 0; i < hinie_attributes.Length; i++)
			{
				hinie_attributes[i].SetParent(this);

				switch (hinie_attributes[i].type)
				{
					case Attribute_Type.HP:
						hinie_attributes[i].value.BaseValue = hinie_base_stats.hero_HP.value.BaseValue;
						break;
					case Attribute_Type.AP:
						hinie_attributes[i].value.BaseValue = hinie_base_stats.hero_AP.value.BaseValue;
						break;
					case Attribute_Type.DEF:
						hinie_attributes[i].value.BaseValue = hinie_base_stats.hero_DEF.value.BaseValue;
						break;
					case Attribute_Type.DMG:
						hinie_attributes[i].value.BaseValue = hinie_base_stats.hero_DMG.value.BaseValue;
						break;
					case Attribute_Type.INT:
						hinie_attributes[i].value.BaseValue = hinie_base_stats.hero_INT.value.BaseValue;
						break;
					case Attribute_Type.JUMP:
						hinie_attributes[i].value.BaseValue = hinie_base_stats.hero_JUMP.value.BaseValue;
						break;
					default:
						break;
				}
			}
		}


		UpdateStatsDisplay();
	}

	private void Start()
	{

		/*
		for (int i = 0; i < player_inventory.GetSlots.Length; i++)
		{
			player_inventory.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
			player_inventory.GetSlots[i].OnBeforeUpdate += OnAfterSlotUpdate;
		}
		*/
	}

	public void OnBeforeSlotUpdate(InventorySlot _slot)
	{
		if (_slot.ItemObject == null)
		{
			return;
		}
		switch (_slot.parent_interface.inventory.type)
		{
			case InterfaceType.Inventory:
				break;
			case InterfaceType.Equipment:
				print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent_interface.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.allowed_items)));

				if (_slot.allowed_items[0] == ItemType.Chestplate || _slot.allowed_items[0] == ItemType.Gloves || _slot.allowed_items[0] == ItemType.Spear)
				{
					for (int i = 0; i < _slot.item.buffs.Length; i++)
					{
						for (int j = 0; j < spearman_attributes.Length; j++)
						{
							if (spearman_attributes[j].type == _slot.item.buffs[i].attribute)
							{
								spearman_attributes[j].value.RemoveModifier(_slot.item.buffs[i]);

								if (_slot.item.buffs[i].attribute == Attribute_Type.HP)
								{
									//StatHolder.spearman_max_HP = attributes[i].value.ModifiedValue;
									StatHolder.ChangeMaxHP(spearman_attributes[i].value.ModifiedValue);
								}
							}
						}
					}
				}

				if (_slot.allowed_items[0] == ItemType.Helmet || _slot.allowed_items[0] == ItemType.Boots)
				{
					for (int i = 0; i < _slot.item.buffs.Length; i++)
					{
						for (int j = 0; j < hinie_attributes.Length; j++)
						{
							if (hinie_attributes[j].type == _slot.item.buffs[i].attribute)
							{
								hinie_attributes[j].value.RemoveModifier(_slot.item.buffs[i]);

								if (_slot.item.buffs[i].attribute == Attribute_Type.HP)
								{
									//StatHolder.spearman_max_HP = attributes[i].value.ModifiedValue;
									StatHolder.ChangeMaxHP(hinie_attributes[i].value.ModifiedValue);
								}
							}
						}
					}
				}

				break;
			case InterfaceType.Mental:
				break;
			case InterfaceType.Chest:
				break;
			default:
				break;
		}
		hp_display.ChangeStatDisplay();
		UpdateStatsDisplay();
	}

	public void OnAfterSlotUpdate(InventorySlot _slot)
	{
		if (_slot.ItemObject == null)
		{
			return;
		}

		switch (_slot.parent_interface.inventory.type)
		{
			case InterfaceType.Inventory:
				break;
			case InterfaceType.Equipment:
				print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent_interface.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.allowed_items)));

				if (_slot.allowed_items[0] == ItemType.Chestplate || _slot.allowed_items[0] == ItemType.Gloves || _slot.allowed_items[0] == ItemType.Spear)
				{
					for (int i = 0; i < _slot.item.buffs.Length; i++)
					{
						for (int j = 0; j < spearman_attributes.Length; j++)
						{
							if (spearman_attributes[j].type == _slot.item.buffs[i].attribute)
							{
								spearman_attributes[j].value.AddModifier(_slot.item.buffs[i]);

								if (_slot.item.buffs[i].attribute == Attribute_Type.HP)
								{
									//StatHolder.spearman_max_HP = attributes[i].value.ModifiedValue;
									StatHolder.ChangeMaxHP(spearman_attributes[i].value.ModifiedValue);
								}
							}
						}
					}
				}

				if (_slot.allowed_items[0] == ItemType.Helmet || _slot.allowed_items[0] == ItemType.Boots)
				{
					for (int i = 0; i < _slot.item.buffs.Length; i++)
					{
						for (int j = 0; j < hinie_attributes.Length; j++)
						{
							if (hinie_attributes[j].type == _slot.item.buffs[i].attribute)
							{
								hinie_attributes[j].value.AddModifier(_slot.item.buffs[i]);

								if (_slot.item.buffs[i].attribute == Attribute_Type.HP)
								{
									//StatHolder.spearman_max_HP = attributes[i].value.ModifiedValue;
									StatHolder.ChangeMaxHP(hinie_attributes[i].value.ModifiedValue);
								}
							}
						}
					}
				}

				break;
			case InterfaceType.Mental:
				break;
			case InterfaceType.Chest:
				break;
			default:
				break;
		}
		hp_display.ChangeStatDisplay();
		UpdateStatsDisplay();
	}

	public void AttributeModified(Attribute attribute)
	{

		Debug.Log(string.Concat(attribute.type, " was updated. Value is now ", attribute.value.ModifiedValue));
	}

	public void UpdateStatsDisplay()
	{
		foreach (Attribute attribute in spearman_attributes)
			attribute.UpdateStatDisplay();
		foreach (Attribute attribute1 in hinie_attributes)
			attribute1.UpdateStatDisplay();
	}

	private void OnApplicationQuit()
	{
		player_inventory.container.Clear();
		player_inventory.usables_inventory.container.Clear();
		player_equipment.container.Clear();

		foreach (InventorySlot mental_slot in mental_inventory.GetSlots)
		{
			if (mental_slot.slot_mental_act != null)
				mental_slot.slot_mental_act.mental_applied = false;
		}

		mental_inventory.container.Clear();
		mental_choose.container.Clear();
		//player_base_stats.Clear();
	}

	public void SaveInventory()
	{
		player_inventory.Save();
		player_equipment.Save();
		mental_inventory.Save();
		player_inventory.usables_inventory.Save();
		mental_choose.Save();

		//добавить сюда метод сохранения других показателей игры
	}

	public void LoadInventory()
	{
		player_inventory.Load();
		player_equipment.Load();
		mental_inventory.Load();
		player_inventory.usables_inventory.Load();
		mental_choose.Load();

		//добавить сюда метод загрузка других показателей игры
	}

	public void ResetInventory()
	{
		player_inventory.container.Clear();
		player_inventory.usables_inventory.container.Clear();
		player_equipment.container.Clear();

		foreach (InventorySlot mental_slot in mental_inventory.GetSlots)
		{
			if (mental_slot.slot_mental_act != null)
				mental_slot.slot_mental_act.mental_applied = false;
		}

		mental_inventory.container.Clear();
		//mental_choose.container.Clear();
	}
}

[System.Serializable]
public class Attribute
{
	[System.NonSerialized]
	public InventoryHolder parent;
	public Attribute_Type type;
	public ModifiableInt value;
	public Stat_Display this_stat_display;

	public void SetParent(InventoryHolder _parent)
	{
		parent = _parent;
		value = new ModifiableInt(AttributeModified);
	}
	public void AttributeModified()
	{
		parent.AttributeModified(this);
	}

	public void UpdateStatDisplay()
	{
		/*
		if (this_stat_display.max_hp_display != null)
		{
			if (this_stat_display.spearman)
			{
				this_stat_display.stat_display.text = StatHolder.spearman_current_HP.ToString();
				this_stat_display.max_hp_display.text = StatHolder.spearman_max_HP.ToString();
			}

			else
			{
				this_stat_display.stat_display.text = StatHolder.hinie_current_HP.ToString();
				this_stat_display.max_hp_display.text = StatHolder.hinie_max_Hp.ToString();
			}
			return;
		}
		
		else */
		if (this_stat_display != null)
		{
			this_stat_display.stat_display.text = this.value.ModifiedValue.ToString();
			return;
		}
	}
}