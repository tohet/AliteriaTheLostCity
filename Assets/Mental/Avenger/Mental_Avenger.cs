using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Avenger", menuName = "Mental/Avenger")]
public class Mental_Avenger : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	int hero_HP_by_the_start;
	public override void DoMental(bool mode_is_explore)
	{
		hero_HP_by_the_start = StatHolder.spearman_current_HP;
	}
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.DamageTaken)
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (playerOBJ.GetComponent<Hero_Stats>().spearman)
				{
					int damage_to_add = hero_HP_by_the_start - playerOBJ.GetComponent<Hero_Stats>().hero_current_HP;
					playerOBJ.GetComponent<Hero_Stats>().heroDamage += damage_to_add;
					hero_HP_by_the_start = playerOBJ.GetComponent<Hero_Stats>().hero_current_HP;
				}
			}

		}
	}
}
