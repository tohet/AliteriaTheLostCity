using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue_Button_ID : MonoBehaviour
{
	public bool is_pushable = true;
	public int next_line_index;
	public bool opens_path;
	public bool opens_exit;
	public Attribute_Type to_change_attribute;
	public int to_change_attribute_value;

	public bool has_item_interaction;
	public ItemObject item_to_interact;
	public int added_or_removed_item_amount;

	public bool loads_level;
	public bool gives_mental_choose;
	public string level_name;
	public bool launches_custom_code;
	public List<string> objects_launched_from_dialogue;

	public ItemObject mental_to_interact;

	public TextBlock parent_text_block;
	public GameObject dialogue_manager;

	public void ChooseAnswer()
	{
		if (is_pushable)
		{
			/*
			GameObject[] dialogue_buttons = GameObject.FindGameObjectsWithTag("dialogue_button");

			foreach (GameObject dialogue_button in dialogue_buttons)
			{
				dialogue_button.GetComponent<Dialogue_Button_ID>().is_pushable = false;
			}
			*/

			Dialogue_Mngr manager = dialogue_manager.GetComponent<Dialogue_Mngr>();
			manager.objectUsed = true;

			manager.text_block_template.GetComponent<TextBlock>().answer_options = manager.dialogue_lines.ReturnAnswerOptions(next_line_index);

			manager.dialogue_line_index = next_line_index;

			foreach (Dialogue_Button_ID previous_answer_button in parent_text_block.answer_buttons)
			{
				previous_answer_button.is_pushable = false;
			}

			if (this.has_item_interaction)
			{
				ItemInteract(item_to_interact, added_or_removed_item_amount);
			}

			if (this.mental_to_interact != null)
			{
				inventoryObj player_mental_inventory = dialogue_manager.GetComponent<Dialogue_Mngr>().inventory_holder.GetComponent<InventoryHolder>().mental_inventory;
				if (player_mental_inventory.AddItem(mental_to_interact.data, 1))
				{
					Debug.Log("Added a mental");
				}
			}

			StatChange(to_change_attribute, to_change_attribute_value);

			if (launches_custom_code)
			{
				foreach (string object_name in objects_launched_from_dialogue)
				{
					GameObject.Find(object_name).GetComponent<ICustomDialogueScript>().RunFromDialogue();
				}
			}

			if (opens_exit)
			{
				dialogue_manager.GetComponent<Dialogue_Mngr>().level_exit.SetActive(true);
			}

			if (opens_path)
			{
				manager.path_opened = opens_path;
			}

			if (loads_level)
			{
				SceneManager.LoadScene(level_name);
				Destroy(dialogue_manager);
			}

			if (gives_mental_choose)
			{
				GameObject mental_choose = Instantiate(dialogue_manager.GetComponent<Dialogue_Mngr>().mental_choose_Filler);
				//mental_choose.GetComponent<MChFiller>().mental_choises.InitializeSlots();
				mental_choose.GetComponent<MChFiller>().next_line_index = next_line_index;
				Destroy(dialogue_manager);
			}

			if (next_line_index == 0)
			{
				if (manager.path_opened)
				{
					pause_menu pause_Menu = GameObject.FindGameObjectWithTag("pause_menu").GetComponent<pause_menu>();
					pause_Menu.CallPathMap();
				}

				Usable_Object.dialogue_window_called = false;

				if (dialogue_manager.GetComponent<Dialogue_Mngr>().used_object.GetComponent<Usable_Object>() != null)
				{
					if (dialogue_manager.GetComponent<Dialogue_Mngr>().used_object.GetComponent<Usable_Object>().delete_after_use)
					{
						Destroy(dialogue_manager.GetComponent<Dialogue_Mngr>().used_object);
					}
					dialogue_manager.GetComponent<Dialogue_Mngr>().used_object.GetComponent<Usable_Object>().SwitchToPlayerCamera();
				}

				//ВОЗВРАТ К КАМЕРЕ ИГРОКА ЗДЕСЬ
				
				StatHolder.dialogue_window_called = false;
				Destroy(dialogue_manager);
				return;
			}
				
			manager.AddTextBlock();

			manager.dialogue_lines.ReturnAnswerOptions(manager.dialogue_line_index);

			//Dialogue_Lines.AssignAnswerOptions(next_line_index);

			//is_pushable = false;
		}

	}

	public void StatChange(Attribute_Type _to_change_attribute, int _to_change_attribute_value)
	{
		Attribute[] attributes_to_change = dialogue_manager.GetComponent<Dialogue_Mngr>().inventory_holder.GetComponent<InventoryHolder>().spearman_attributes;

		foreach (Attribute attribute in attributes_to_change)
		{
			if (attribute.type == _to_change_attribute)
			{
				if (_to_change_attribute == Attribute_Type.HP)
				{
					StatHolder.spearman_current_HP += _to_change_attribute_value;
					GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
					return;
				}
				else
				{
					attribute.value.BaseValue += _to_change_attribute_value;
				}
			}
		}

		if (StatHolder.spearman_current_HP <= 0 && StatHolder.hinie_current_HP <= 0)
		{
			Destroy(GameObject.FindGameObjectWithTag("pause menu"));
			SceneManager.LoadScene("intro");
		}
	}

	public void ItemInteract(ItemObject item_to_interact, int _added_or_removed_item_amount)
	{
		inventoryObj player_inventory = dialogue_manager.GetComponent<Dialogue_Mngr>().inventory_holder.GetComponent<InventoryHolder>().player_inventory;
		Item added_or_removed_item = new Item(item_to_interact);

		player_inventory.AddItem(added_or_removed_item, added_or_removed_item_amount);
		/*
		if (player_inventory.EmptySlotCount <= 0)
		{
			return;
		}

		InventorySlot slot = player_inventory.FindItemOnInventory(added_or_removed_item);

		if (!player_inventory.dataBase.getItem[added_or_removed_item.id].stackable || slot == null)
		{
			player_inventory.SetEmptySlot(added_or_removed_item, _added_or_removed_item_amount);
			return;
		}

		slot.AddAmmount(_added_or_removed_item_amount);
		
		if (slot.ammount <= 0)
		{
			slot.RemoveItem();
		}
		*/
		return;

	}
}
