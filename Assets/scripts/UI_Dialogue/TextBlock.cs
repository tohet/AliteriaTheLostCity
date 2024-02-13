using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlock : MonoBehaviour
{
	public GameObject dialogue_managerOBJ;
	Dialogue_Mngr dialogue_manager;

	public Text dialogue_NPC_text;
	public Answer_Class[] answer_options;
	public List<Dialogue_Button_ID> answer_buttons = new List<Dialogue_Button_ID>();
	public int[] next_dialogue_line_index;

	private void Start()
	{
		//dialogue_managerOBJ = GameObject.FindGameObjectWithTag("dialogue_menu");
		dialogue_manager = dialogue_managerOBJ.GetComponent<Dialogue_Mngr>();

		dialogue_NPC_text.text = dialogue_manager.dialogue_lines.dialogue_lines[dialogue_manager.dialogue_line_index];
		answer_options = dialogue_manager.dialogue_lines.ReturnAnswerOptions(dialogue_manager.dialogue_line_index);
		/*
		dialogue_NPC_text.text = Dialogue_Lines.dialogue_lines_desert_1[dialogue_manager.dialogue_line_index];
		answer_options = Dialogue_Lines.AssignAnswerOptions(dialogue_manager.dialogue_line_index);
		*/
	}
}

