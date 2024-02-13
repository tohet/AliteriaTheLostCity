using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatueSceneLogic : ICustomDialogueScript
{
	public Usable_Object statue;
	public override void RunFromDialogue()
	{
		StoryProgression.used_statue = true;
	}

	void Start()
    {
		if (StoryProgression.used_statue)
			statue.dialogue_line_index = 8;
    }
}
