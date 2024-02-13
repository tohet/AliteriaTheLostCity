using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Outline : MonoBehaviour
{
	[SerializeField] private GameObject[] targets;
	public bool auto_use;
	private void OnTriggerEnter(Collider other)
	{
		foreach (GameObject target in targets)
		{
			if (auto_use)
			{
				target.SendMessage("Use", SendMessageOptions.DontRequireReceiver);
			}
			target.SendMessage("OutlineON");
		}
	}

	private void OnTriggerExit(Collider other)
	{
		foreach (GameObject target in targets)
		{
			target.SendMessage("OutlineOFF");
		}
	}
}
