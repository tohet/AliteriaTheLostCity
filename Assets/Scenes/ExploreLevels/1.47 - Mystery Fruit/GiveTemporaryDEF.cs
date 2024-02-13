using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveTemporaryDEF : ICustomDialogueScript
{
	public int gained_DEF;
	public override void RunFromDialogue()
	{
		StatHolder.spearman_armor_bonus += gained_DEF;
	}
}
