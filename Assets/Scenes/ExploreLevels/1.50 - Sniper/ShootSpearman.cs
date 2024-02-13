using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpearman : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		StatHolder.spearman_hit_in_knee = true;
	}
}
