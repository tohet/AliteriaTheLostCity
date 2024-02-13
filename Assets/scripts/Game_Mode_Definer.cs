using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Game_Mode_Definer
{
	public static bool Gamemode_Explore()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		return player.TryGetComponent<Hero>(out Hero component);
		/*
		if (player.TryGetComponent<Hero>(out Hero component))
			return true;
		else
			return false;
		*/
	}

	public static bool Gamemode_Explore(out GameObject player)
	{
		player = GameObject.FindGameObjectWithTag("Player");

		if (player.GetComponent<Hero>() != null)
			return true;
		else
			return false;
	}
}
