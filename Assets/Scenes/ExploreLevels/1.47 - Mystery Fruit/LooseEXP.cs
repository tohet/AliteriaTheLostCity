using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseEXP : ICustomDialogueScript
{
	public int lost_EXP;
	public override void RunFromDialogue()
	{
		StatHolder.hero_EXP -= lost_EXP;
	}
}
