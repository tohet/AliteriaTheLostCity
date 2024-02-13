using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Dialogue_Mngr : MonoBehaviour
{
	public DL_SO dialogue_lines;

	public GameObject used_object;

	public GameObject level_exit;

	public GameObject mental_choose_Filler;

	public Button button_template;
	public GameObject text_block_template;
	public GameObject content_block;
	public GameObject inventory_holder;

	bool dialogue_over = false;
	bool first_textblock_added = false;
	public bool objectUsed = false;

	public int dialogue_line_index;

	public Scrollbar dialogue_scrollbar;

	public bool path_opened;

	private void Start()
	{
		SceneManager.sceneUnloaded += OnSceneUnloaded;
		inventory_holder = GameObject.FindGameObjectWithTag("inventory_holder");
	}
	private void Update()
	{
		if (!dialogue_over && !first_textblock_added && objectUsed)
		{
			first_textblock_added = true;
			AddTextBlock();
			dialogue_scrollbar.value = -3f;
			objectUsed = false;
		}
	}

	void OnSceneUnloaded(Scene current)
	{
		Usable_Object.dialogue_window_called = false;
		/*
		if (gameObject.name == "dialogue_menu(Clone)")
		{
			Usable_Object.dialogue_window_called = false;
			Destroy(gameObject);
		}
		*/
	}
	public void AddTextBlock()
	{
		//Добавляет блок текста с вариантом ответа в объект "content"
		TextBlock text_block /*= new TextBlock()*/;
		text_block = Instantiate(text_block_template, content_block.transform).GetComponent<TextBlock>();


		Button[] answers = new Button[text_block_template.GetComponent<TextBlock>().answer_options.Length];
		int answers_ammount = text_block_template.GetComponent<TextBlock>().answer_options.Length;

		for (int i = 0; i < answers_ammount; i++)
		{
			if (text_block_template.GetComponent<TextBlock>().answer_options[i].needed_mental == null && text_block_template.GetComponent<TextBlock>().answer_options[i].needed_item_ID == 0 && StatIsEnough(text_block_template.GetComponent<TextBlock>().answer_options[i])) //0 стоит временно - потом заменить на -1 - а потом вообще поставить ItemObject вместо ID
			{
				Button new_button = Instantiate(button_template, content_block.transform);

				new_button.GetComponentInChildren<Text>().text = text_block_template.GetComponent<TextBlock>().answer_options[i].answer_text;

				Dialogue_Button_ID new_button_ID = new_button.GetComponent<Dialogue_Button_ID>();
				Answer_Class template_answer = text_block_template.GetComponent<TextBlock>().answer_options[i];

				new_button_ID.next_line_index = template_answer.next_line_index;
				new_button_ID.opens_exit = template_answer.opens_exit;
				new_button_ID.opens_path = template_answer.path_proceed;

				new_button_ID.to_change_attribute = template_answer.this_stat_influence;
				new_button_ID.to_change_attribute_value = template_answer.this_stat_influence_value;

				new_button_ID.has_item_interaction = template_answer.has_item_interaction;
				new_button_ID.item_to_interact = template_answer.answ_item_to_interact;
				new_button_ID.added_or_removed_item_amount = template_answer.answ_added_or_removed_item_amount;

				new_button_ID.loads_level = template_answer.loads_level;
				new_button_ID.gives_mental_choose = template_answer.gives_mental_choose;
				new_button_ID.level_name = template_answer.level_name;
				new_button_ID.launches_custom_code = template_answer.launches_custom_code;
				new_button_ID.objects_launched_from_dialogue = template_answer.objects_launched_from_dialogue;

				new_button_ID.mental_to_interact = template_answer.answ_mental_to_interact;

				new_button_ID.parent_text_block = text_block;
				text_block.answer_buttons.Add(new_button.GetComponent<Dialogue_Button_ID>());
				//int next_line_index = i + text_block_template.GetComponent<TextBlock>().answer_options[0].next_line_index;
				//new_button.onClick.AddListener(() => ChooseAnswer(new_button.GetComponent<Dialogue_Button_ID>().next_line_index));
			}

			else if (StatIsEnough(text_block_template.GetComponent<TextBlock>().answer_options[i]))
			{
				if (text_block_template.GetComponent<TextBlock>().answer_options[i].needed_item_ID != 0)
				{
					bool passed_before_usables = false;
					GameObject inventory_holder = GameObject.FindGameObjectWithTag("inventory_holder");
					inventoryObj player_inventory = inventory_holder.GetComponent<InventoryHolder>().player_inventory;

					for (int j = 0; j < player_inventory.container.slots.Length; j++)
					{
						if (player_inventory.container.slots[j].item.id == text_block_template.GetComponent<TextBlock>().answer_options[i].needed_item_ID && player_inventory.container.slots[j].ammount >= text_block_template.GetComponent<TextBlock>().answer_options[i].needed_item_ammount)
						{
							Button new_button = Instantiate(button_template, content_block.transform);

							new_button.GetComponentInChildren<Text>().text = text_block_template.GetComponent<TextBlock>().answer_options[i].answer_text;

							Dialogue_Button_ID new_button_ID = new_button.GetComponent<Dialogue_Button_ID>();
							Answer_Class template_answer = text_block_template.GetComponent<TextBlock>().answer_options[i];

							new_button_ID.next_line_index = template_answer.next_line_index;
							new_button_ID.opens_exit = template_answer.opens_exit;
							new_button_ID.opens_path = template_answer.path_proceed;

							new_button_ID.to_change_attribute = template_answer.this_stat_influence;
							new_button_ID.to_change_attribute_value = template_answer.this_stat_influence_value;

							new_button_ID.has_item_interaction = template_answer.has_item_interaction;
							new_button_ID.item_to_interact = template_answer.answ_item_to_interact;
							new_button_ID.added_or_removed_item_amount = template_answer.answ_added_or_removed_item_amount;

							new_button_ID.loads_level = template_answer.loads_level;
							new_button_ID.gives_mental_choose = template_answer.gives_mental_choose;
							new_button_ID.level_name = template_answer.level_name;
							new_button_ID.launches_custom_code = template_answer.launches_custom_code;
							new_button_ID.objects_launched_from_dialogue = template_answer.objects_launched_from_dialogue;

							new_button_ID.mental_to_interact = template_answer.answ_mental_to_interact;

							new_button.GetComponent<Dialogue_Button_ID>().parent_text_block = text_block;
							text_block.answer_buttons.Add(new_button.GetComponent<Dialogue_Button_ID>());
							passed_before_usables = true;
							break;
						}
						
					}
					
					for (int j = 0; j < player_inventory.usables_inventory.container.slots.Length; j++)
					{
						if ((player_inventory.usables_inventory.container.slots[j].item.id == text_block_template.GetComponent<TextBlock>().answer_options[i].needed_item_ID) && player_inventory.container.slots[j].ammount >= text_block_template.GetComponent<TextBlock>().answer_options[i].needed_item_ammount && !passed_before_usables)
						{
							Button new_button = Instantiate(button_template, content_block.transform);

							new_button.GetComponentInChildren<Text>().text = text_block_template.GetComponent<TextBlock>().answer_options[i].answer_text;

							Dialogue_Button_ID new_button_ID = new_button.GetComponent<Dialogue_Button_ID>();
							Answer_Class template_answer = text_block_template.GetComponent<TextBlock>().answer_options[i];

							new_button_ID.next_line_index = template_answer.next_line_index;
							new_button_ID.opens_exit = template_answer.opens_exit;
							new_button_ID.opens_path = template_answer.path_proceed;

							new_button_ID.to_change_attribute = template_answer.this_stat_influence;
							new_button_ID.to_change_attribute_value = template_answer.this_stat_influence_value;

							new_button_ID.has_item_interaction = template_answer.has_item_interaction;
							new_button_ID.item_to_interact = template_answer.answ_item_to_interact;
							new_button_ID.added_or_removed_item_amount = template_answer.answ_added_or_removed_item_amount;

							new_button_ID.loads_level = template_answer.loads_level;
							new_button_ID.gives_mental_choose = template_answer.gives_mental_choose;
							new_button_ID.level_name = template_answer.level_name;
							new_button_ID.launches_custom_code = template_answer.launches_custom_code;
							new_button_ID.objects_launched_from_dialogue = template_answer.objects_launched_from_dialogue;

							new_button_ID.mental_to_interact = template_answer.answ_mental_to_interact;

							new_button.GetComponent<Dialogue_Button_ID>().parent_text_block = text_block;
							text_block.answer_buttons.Add(new_button.GetComponent<Dialogue_Button_ID>());
							break;
						}
					}
				}

				else if (text_block_template.GetComponent<TextBlock>().answer_options[i].needed_mental != null)
				{
					GameObject inventory_holder = GameObject.FindGameObjectWithTag("inventory_holder");
					inventoryObj mental_inventory = inventory_holder.GetComponent<InventoryHolder>().mental_inventory;

					for (int j = 0; j < mental_inventory.container.slots.Length; j++)
					{
						if (mental_inventory.container.slots[j].ItemObject == text_block_template.GetComponent<TextBlock>().answer_options[i].needed_mental)
						{
							Button new_button = Instantiate(button_template, content_block.transform);

							new_button.GetComponentInChildren<Text>().text = text_block_template.GetComponent<TextBlock>().answer_options[i].answer_text;

							Dialogue_Button_ID new_button_ID = new_button.GetComponent<Dialogue_Button_ID>();
							Answer_Class template_answer = text_block_template.GetComponent<TextBlock>().answer_options[i];

							new_button_ID.next_line_index = template_answer.next_line_index;
							new_button_ID.opens_exit = template_answer.opens_exit;
							new_button_ID.opens_path = template_answer.path_proceed;

							new_button_ID.to_change_attribute = template_answer.this_stat_influence;
							new_button_ID.to_change_attribute_value = template_answer.this_stat_influence_value;

							new_button_ID.has_item_interaction = template_answer.has_item_interaction;
							new_button_ID.item_to_interact = template_answer.answ_item_to_interact;
							new_button_ID.added_or_removed_item_amount = template_answer.answ_added_or_removed_item_amount;

							new_button_ID.loads_level = template_answer.loads_level;
							new_button_ID.gives_mental_choose = template_answer.gives_mental_choose;
							new_button_ID.level_name = template_answer.level_name;
							new_button_ID.launches_custom_code = template_answer.launches_custom_code;
							new_button_ID.objects_launched_from_dialogue = template_answer.objects_launched_from_dialogue;

							new_button_ID.mental_to_interact = template_answer.answ_mental_to_interact;

							new_button.GetComponent<Dialogue_Button_ID>().parent_text_block = text_block;
							text_block.answer_buttons.Add(new_button.GetComponent<Dialogue_Button_ID>());
							break;
						}
					}
				}

			}

		}



		/*
		float x_pos = content_block.GetComponent<RectTransform>().position.x;
		float scroll_value = content_block.GetComponent<RectTransform>().position.y;
		content_block.GetComponent<RectTransform>().transform.position = new Vector2(x_pos, scroll_value + 1000000);
		*/
	}

	bool StatIsEnough(Answer_Class answer_option)
	{
		foreach (Attribute attribute in inventory_holder.GetComponent<InventoryHolder>().spearman_attributes)
		{
			if (answer_option.needed_attribute == attribute.type)
			{
				if (answer_option.needed_attribute_value <= attribute.value.ModifiedValue)
				{
					return true;
				}

				else return false;
			}
		}

		return false;
	}
	/*
	void ChooseAnswer(int answer_next_line_index)
	{
		dialogue_line_index = answer_next_line_index;

		AddTextBlock();
		Dialogue_Lines.AssignAnswerOptions(dialogue_line_index);
		dialogue_scrollbar.value = -3f;

	}
	*/
}
