using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public static class StatHolder
{
	public static bool dialogue_window_called = false;

	public static int spearman_current_HP = 100;
	public static int hinie_current_HP = 80;
	public static int spearman_armor_bonus = 0;

	public static bool spearman_hit_in_knee = false;

	public static int spearman_max_HP = 100;
	public static int hinie_max_Hp = 80;
	public static int hinie_armor_bonus = 0;

	public static bool hinie_hit_in_knee = false;

	public static int hero_level = 1;
	public static int hero_EXP = 0;
	public static int exp_to_level_up = 300;
	public static bool level_up = false;

	public static int hp_lvl = 1;
	public static int ap_lvl = 1;
	public static int def_lvl = 1;
	public static int dmg_lvl = 1;
	public static int int_lvl = 1;

	public static double exp_multiplier = 1;

	public static int use_potion_AP_cost = 2;
	public static void ChangeCurrentHP(int spearman_HP, int hinie_HP)
	{
		
		if (spearman_HP > spearman_max_HP)
		{
			spearman_current_HP = spearman_max_HP;
		}
		else
		{
			spearman_current_HP = spearman_HP;
		}
		
		//spearman_current_HP = spearman_HP <= spearman_max_HP ? spearman_HP : spearman_max_HP;
		
		//hinie_current_HP = hinie_HP;

		if (hinie_HP > hinie_max_Hp)
		{
			hinie_current_HP = hinie_max_Hp;
		}
		else
		{
			hinie_current_HP = hinie_HP;
		}
	}

	public static void ChangeMaxHP(int spearman_MAX)
	{
		spearman_max_HP = spearman_MAX;
	}

	public static void CheckHealth()
	{
		if (spearman_current_HP <= 0)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("intro");
		}
	}

	public static void ResetStats()
	{
	dialogue_window_called = false;

	spearman_current_HP = 100;
	hinie_current_HP = 80;

	spearman_max_HP = 100;
	hinie_max_Hp = 80;

		spearman_hit_in_knee = false;
		hinie_hit_in_knee = false;

		spearman_armor_bonus = 0;
		hinie_armor_bonus = 0;
		/*
	hinie_MAX_HP = 75;
	hinie_HP = 75;
	hinie_AP = 5;
	hinie_DEF = 0;
	hinie_DMG = 60;
	hinie_JUMP = 0;
	hinie_INT = 0;
		*/

	hero_level = 1;
	hero_EXP = 0;
	exp_to_level_up = 300;
	level_up = false;

	hp_lvl = 1;
	ap_lvl = 1;
	def_lvl = 1;
	dmg_lvl = 1;
	int_lvl = 1;
	use_potion_AP_cost = 2;
	}

	public static void IncreaceEXP(int exp)
	{
		hero_EXP += exp;
		Debug.Log("Hero EXP is " + hero_EXP);
		if (hero_EXP >= exp_to_level_up)
		{
			hero_level++;
			level_up = true;
			Debug.Log("Hero reached level " + hero_level);
			exp_to_level_up *= 2;
		}
	}
}
