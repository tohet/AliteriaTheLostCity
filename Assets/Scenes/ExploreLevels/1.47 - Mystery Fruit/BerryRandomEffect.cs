using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryRandomEffect : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		Dialogue_Mngr dialogue_window = GameObject.Find("dialogue_menu(Clone)").GetComponent<Dialogue_Mngr>();

		int rnd = Random.Range(1, 10);

		switch (rnd)
		{
			case 1:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(2);
				dialogue_window.dialogue_line_index = 2;
				break;
			case 2:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(3);
				dialogue_window.dialogue_line_index = 3;
				break;
			case 3:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(4);
				dialogue_window.dialogue_line_index = 4;
				break;
			case 4:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(5);
				dialogue_window.dialogue_line_index = 5;
				break;
			case 5:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(6);
				dialogue_window.dialogue_line_index = 6;
				break;
			case 6:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(6);
				dialogue_window.dialogue_line_index = 6;
				break;
			case 7:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(7);
				dialogue_window.dialogue_line_index = 7;
				break;
			case 8:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(8);
				dialogue_window.dialogue_line_index = 8;
				break;
			case 9:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(6);
				dialogue_window.dialogue_line_index = 6;
				break;
			case 10:
				dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(5);
				dialogue_window.dialogue_line_index = 5;
				break;
			default:
				break;
		}
	}
}
