using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeenWhales : ICustomDialogueScript
{
	public Dialogue_Mngr dialogue_window;
	public override void RunFromDialogue()
	{
		dialogue_window = GameObject.Find("dialogue_menu(Clone)").GetComponent<Dialogue_Mngr>();

		if (StoryProgression.saw_sand_whales)
		{
			dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(10);
			dialogue_window.dialogue_line_index = 10;
		}

		else
		{
			dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(11);
			dialogue_window.dialogue_line_index = 11;
		}
	}
}
