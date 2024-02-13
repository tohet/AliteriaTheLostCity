using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Holder : MonoBehaviour
{
	public DL_SO this_scene_dialogues;
	public DL_SO russian_dialogues;
	public GameObject level_exit;

	public void Awake()
	{
		
		GameObject dialogue_menu = GameObject.FindGameObjectWithTag("dialogue_menu");

		if (StoryProgression.russian_language_picked && russian_dialogues != null)
			dialogue_menu.GetComponent<Dialogue_Mngr>().dialogue_lines = russian_dialogues;
		else
			dialogue_menu.GetComponent<Dialogue_Mngr>().dialogue_lines = this_scene_dialogues;
		
		MentalCaller.CallMentals();

		if (dialogue_menu.GetComponent<Dialogue_Mngr>().level_exit == null && level_exit != null)
		{
			dialogue_menu.GetComponent<Dialogue_Mngr>().level_exit = level_exit;
		}
	}
}
