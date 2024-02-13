using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDSTRakeLogic : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		StoryProgression.rake_hits++;
		StatHolder.IncreaceEXP(System.Convert.ToInt32(150 * StatHolder.exp_multiplier));
	}
}
