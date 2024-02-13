using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Answer_Class
{
	public int corelated_dialogue_line_ID = -1;
	public string answer_text;
	public int next_line_index;

	//это действия, которые выбор ответа совершает по отношению к внешнему миру (загрузка узлов/вызов генерации пути/предоставление выбора менталей и т.д.)
	public bool path_proceed;
	public bool opens_exit;
	public bool loads_level;
	public bool gives_mental_choose;
	public string level_name;
	//эта переменная определяет, запускает ли ответ кастомный код в сцене
	public bool launches_custom_code;
	[SerializeField]
	public List<string> objects_launched_from_dialogue;

	//это небходимые условия для того, чтобы вариант диалога появился
	public int needed_item_ID = -1;
	public int needed_item_ammount = 0;
	public ItemObject needed_mental;
	public Attribute_Type needed_attribute;
	public int needed_attribute_value;

	//это влияния, которые выбор варианта ответа оказывает на героя
	public Attribute_Type this_stat_influence;
	public int this_stat_influence_value;
	public bool has_item_interaction;
	public ItemObject answ_item_to_interact;
	public int answ_added_or_removed_item_amount;
	public ItemObject answ_mental_to_interact;

	public bool dialogue_end;
	
	public Answer_Class(string text, int next_ind, bool _path_proceed, int _needed_item_ID, ItemObject _needed_mental)
	{
		answer_text = text;
		next_line_index = next_ind;
		path_proceed = _path_proceed;
		needed_item_ID = _needed_item_ID;
		needed_item_ammount = 0;
		needed_mental = _needed_mental;
	}
	public Answer_Class(string text, int next_ind, bool _path_proceed, int _needed_item_ID)
	{
		answer_text = text;
		next_line_index = next_ind;
		path_proceed = _path_proceed;
		needed_item_ID = _needed_item_ID;
	}
	public Answer_Class(string text, int next_ind, bool _path_proceed)
	{
		answer_text = text;
		next_line_index = next_ind;
		path_proceed = _path_proceed;
	}
	public Answer_Class(string text, int next_ind)
	{
		answer_text = text;
		next_line_index = next_ind;
		path_proceed = false;
	}
	
}
