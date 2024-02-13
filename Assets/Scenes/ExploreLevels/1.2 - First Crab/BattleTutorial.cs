using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTutorial : MonoBehaviour
{
	public GameObject[] tutorial_windows;
	public GameObject[] tutorial_arrows;
	GameObject pause_menu;
	public int window_index = 0;
	private void Awake()
	{
		pause_menu = GameObject.FindGameObjectWithTag("pause_menu");
		Instantiate(tutorial_windows[window_index], pause_menu.transform);
		tutorial_arrows[window_index].SetActive(true);
		//window_index++;
	}

	public void ChainWindowSpawn()
	{
		window_index++;
		if (window_index <= tutorial_windows.Length - 1)
		{
			Instantiate(tutorial_windows[window_index], pause_menu.transform, true);
		}
	}

	public void DoTutorial()
	{

	}
}
