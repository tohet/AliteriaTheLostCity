using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HinieUFD1_3 : ICustomDialogueScript
{
	public Animator animator;
	public override void RunFromDialogue()
	{
		this.gameObject.AddComponent<Rigidbody>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		animator.SetBool("landed", true);
	}
}
