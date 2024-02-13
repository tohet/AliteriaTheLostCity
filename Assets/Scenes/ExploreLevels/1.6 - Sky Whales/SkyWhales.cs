using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyWhales : ICustomDialogueScript
{
	public Camera dialogue_use_cam;
	public Camera this_use_cam;
	public GameObject whales;
	public override void RunFromDialogue()
	{
		whales.SetActive(true);
		dialogue_use_cam.gameObject.SetActive(false);
		this_use_cam.gameObject.SetActive(true);
		StoryProgression.saw_sand_whales = true;
	}
}
