using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarGives : ICustomDialogueScript
{
	int next_line_index;
	public GameObject used_object;
	public override void RunFromDialogue()
	{
		if (Random.Range(1, 10) >= 7)
			next_line_index = 5;
		else
			next_line_index = 6;
		CallDialogueWindow();
	}

	public void CallDialogueWindow()
	{
		GameObject dialogue_UI = GameObject.FindGameObjectWithTag("dialogue_menu");
		GameObject ui_obj = GameObject.FindGameObjectWithTag("pause_menu");

		{
			StatHolder.dialogue_window_called = true;
			GameObject new_dialogue_UI = Instantiate(dialogue_UI, ui_obj.transform);
			new_dialogue_UI.transform.position = new Vector2(1446.1f, 500f);
			new_dialogue_UI.SetActive(true);
			new_dialogue_UI.GetComponent<Dialogue_Mngr>().used_object = used_object;
			new_dialogue_UI.GetComponent<Dialogue_Mngr>().objectUsed = true;
			new_dialogue_UI.GetComponent<Dialogue_Mngr>().dialogue_line_index = next_line_index;
		}
	}
}
