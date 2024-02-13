using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevelZone : MonoBehaviour
{
	[SerializeField] private GameObject[] targets;
	private void OnTriggerEnter(Collider other)
	{
		if (targets.Length == 0)
		{
			GameObject.FindGameObjectWithTag("pause_menu").SendMessage("CallPathMap");
		}
		foreach (GameObject target in targets)
		{
			target.SendMessage("CallPathMap");
		}
	}
}
