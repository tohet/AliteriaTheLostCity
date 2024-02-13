using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnDL : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		Dialogue_Mngr dialogue_window = GameObject.Find("dialogue_menu(Clone)").GetComponent<Dialogue_Mngr>();
		dialogue_window.dialogue_lines = GameObject.Find("Dialogue_Holder").GetComponent<Dialogue_Holder>().this_scene_dialogues;
		dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(0);
		dialogue_window.dialogue_line_index = 0;
	}
}
