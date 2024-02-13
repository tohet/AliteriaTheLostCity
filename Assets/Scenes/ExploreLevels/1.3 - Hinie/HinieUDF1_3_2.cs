using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinieUDF1_3_2 : ICustomDialogueScript
{
	public Animator animator;
	public override void RunFromDialogue()
	{
		animator.SetBool("ready_for_attack", true);
	}
}
