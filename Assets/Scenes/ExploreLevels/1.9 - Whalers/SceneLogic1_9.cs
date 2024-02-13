using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLogic1_9 : MonoBehaviour
{
	private void Awake()
	{
		if (!StoryProgression.met_walers)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 0;
			StoryProgression.met_walers = true;
		}

		else if (!StoryProgression.saw_sand_whales)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 16;
		}

		else if (!StoryProgression.helped_whalers)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 17;
		}

		else
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 16;
		}
	}
}
