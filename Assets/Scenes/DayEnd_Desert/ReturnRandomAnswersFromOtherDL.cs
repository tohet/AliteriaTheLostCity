using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnRandomAnswersFromOtherDL : ICustomDialogueScript
{
	public DL_SO new_dialogue_lines;
	public int[] possible_answer_indexes = new int[] { };
	public int given_answers_amount;
	public override void RunFromDialogue()
	{

		Dialogue_Mngr dialogue_window = GameObject.Find("dialogue_menu(Clone)").GetComponent<Dialogue_Mngr>();
		dialogue_window.dialogue_lines = new_dialogue_lines;//GameObject.Find("Additional_DL_Holder").GetComponent<AddDLs>().world_lines;

		//int[] initialize_answer_indexes = new int[] {0, 6, 10};

		Answer_Class[] world_random_questions = new Answer_Class[given_answers_amount];

		for (int i = 0; i < given_answers_amount; i++)
		{
			// possible_answer_indexes[Random.Range(0, possible_answer_indexes.Length)] <= заменять этот кусок на числовой индекс варианта ответа, который надо проверить - потом заменять обратно на него
			world_random_questions[i] = dialogue_window.dialogue_lines.answer_options[possible_answer_indexes[Random.Range(0, possible_answer_indexes.Length)]].answers[0];
			//	dialogue_window.dialogue_lines.ReturnAnswerOptions(22)[0];
		}

		dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = world_random_questions;
		dialogue_window.dialogue_line_index = 1;
	}
}
