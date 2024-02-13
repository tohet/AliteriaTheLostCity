using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootHinie : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		StatHolder.hinie_hit_in_knee = true;
	}
}
