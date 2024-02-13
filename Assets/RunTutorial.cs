using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTutorial : MonoBehaviour
{
	public GameObject tutorial_window;
	private void OnTriggerEnter(Collider other)
	{
		if (tutorial_window != null)
			tutorial_window.SetActive(true);
		Destroy(this.gameObject);
	}
}
