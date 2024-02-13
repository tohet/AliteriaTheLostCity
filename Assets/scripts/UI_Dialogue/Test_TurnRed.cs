using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TurnRed : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		this.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
	}
}
