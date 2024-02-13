using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Usable_Quit : MonoBehaviour
{
	[SerializeField] private GameObject[] targets;
	private void OnTriggerExit(Collider other)
	{
		foreach (GameObject target in targets)
		{
			target.SendMessage("LeaveAreaQuit");
		}
	}
}
