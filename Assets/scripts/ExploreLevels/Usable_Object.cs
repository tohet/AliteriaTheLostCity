using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Object : MonoBehaviour
{
	public GameObject player_cam;
	public Camera use_cam;
	public static bool dialogue_window_called;
	public int dialogue_line_index;
	public GameObject dialogue_UI;
	public bool one_time_use;
	public bool delete_after_use;
	bool used = false;

	private void Start()
	{
		player_cam = GameObject.FindGameObjectWithTag("platformer_camera");
		dialogue_UI = GameObject.FindGameObjectWithTag("dialogue_menu");
		//dialogue_UI.SetActive(false);
	}
	public void OutlineON()
	{
		if (!used)
			GetComponent<Outline>().enabled = true;
	}

	public void OutlineOFF()
	{
		GetComponent<Outline>().enabled = false;
	}
	public void Use()
	{
		CallDialogueWindow();
	}

	public void CallDialogueWindow()
	{
		if (!used)
		{
			GameObject ui_obj = GameObject.FindGameObjectWithTag("pause_menu");

			if (!dialogue_window_called)
			{
				StatHolder.dialogue_window_called = true;
				dialogue_window_called = true;
				GameObject new_dialogue_UI = Instantiate(dialogue_UI, ui_obj.transform);
				new_dialogue_UI.transform.position = new Vector2(1446.1f, 500f);
				new_dialogue_UI.SetActive(true);
				new_dialogue_UI.GetComponent<Dialogue_Mngr>().used_object = this.gameObject;
				new_dialogue_UI.GetComponent<Dialogue_Mngr>().objectUsed = true;
				new_dialogue_UI.GetComponent<Dialogue_Mngr>().dialogue_line_index = dialogue_line_index;
				if (use_cam != null)
					SwitchToUseCamera();
			}

			if (one_time_use)
				used = true;
		}

	}

	public void SwitchToUseCamera()
	{
		player_cam.SetActive(false);
		use_cam.gameObject.SetActive(true);
	}

	public void SwitchToPlayerCamera()
	{
		use_cam.gameObject.SetActive(false);
		player_cam.SetActive(true);
	}
}
