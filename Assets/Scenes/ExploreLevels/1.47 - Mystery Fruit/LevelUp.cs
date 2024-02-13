using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		StatHolder.IncreaceEXP(StatHolder.exp_to_level_up);
	}
}
