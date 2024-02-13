using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyCrabLogic : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
        StoryProgression.healed_friendly_crab = true;
	}

	void Start()
    {
        if (StoryProgression.healed_friendly_crab)
        {
            gameObject.GetComponent<Usable_Object>().dialogue_line_index = 9;
            return;
        }

        else if (StoryProgression.met_friendly_crab)
        {
            gameObject.GetComponent<Usable_Object>().dialogue_line_index = 6;
            return;
        }

        StoryProgression.met_friendly_crab = true;
    }
}
