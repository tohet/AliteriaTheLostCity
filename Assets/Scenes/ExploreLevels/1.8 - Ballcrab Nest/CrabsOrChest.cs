using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabsOrChest : ICustomDialogueScript
{
	public GameObject chest;

	private void Start()
	{
		chest.SetActive(false);
	}
	public override void RunFromDialogue()
	{
		Dialogue_Mngr dialogue_window = GameObject.Find("dialogue_menu(Clone)").GetComponent<Dialogue_Mngr>();
		if (Random.Range(1, 10) >= 5)
		{
			chest.SetActive(true);
			dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(2);
			dialogue_window.dialogue_line_index = 2;
		}
		else
		{
			dialogue_window.text_block_template.GetComponent<TextBlock>().answer_options = dialogue_window.dialogue_lines.ReturnAnswerOptions(3);
			dialogue_window.dialogue_line_index = 3;
		}
	}
}
