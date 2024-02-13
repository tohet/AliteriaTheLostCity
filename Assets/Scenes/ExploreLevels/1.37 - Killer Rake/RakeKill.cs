using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RakeKill : ICustomDialogueScript
{
	public override void RunFromDialogue()
	{
		SceneManager.LoadScene("intro");
	}
}
