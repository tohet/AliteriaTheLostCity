using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path_mngr : MonoBehaviour
{
	public static int journey_part_colum = 0;
	public static bool pause_menu_instantiated = false;
	public static bool chhest_instantiated = false;
	public static bool inventory_holder_instantiated;

	public static bool ui_called = false;
	public static bool inventory_instantiated = false;

	public static GameObject day_path;
	public static GameObject root;


	public static void AttachNewDayPath(GameObject new_day_path)
	{
		/*
		ammount_of_spawns = 0;
		three_ways = 0;
		two_ways = 0;
		*/
		day_path = new_day_path;
	}
	public static void AttachNewRoot(GameObject new_root)
	{
		root = new_root;
	}
}
