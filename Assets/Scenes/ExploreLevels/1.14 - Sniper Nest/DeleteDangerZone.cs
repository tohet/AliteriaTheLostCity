using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDangerZone : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		this.gameObject.SetActive(false);
	}
}
