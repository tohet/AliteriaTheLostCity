using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornedSceneLogic : ICustomDialogueScript
{
    public Usable_Object horned;
    void Start()
    {
        if (StoryProgression.helped_horned)
        {
            horned.dialogue_line_index = 17;
            return;
        }

        else if (StoryProgression.met_horned)
        {
            horned.dialogue_line_index = 8;
            return;
        }

        StoryProgression.met_horned = true;
    }

	public override void RunFromDialogue()
	{
        //sets helped_horned to true meaning that he gave the helmet away
        StoryProgression.helped_horned = true;
	}
}
