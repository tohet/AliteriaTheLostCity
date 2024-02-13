using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pacifist", menuName = "Mental/Pacifist")]
public class Pacifist : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	bool attacked_last_turn = false;
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.DamageDealt)
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (grid_player.turn && grid_player.thisHeroStats.spearman)
				{
					Debug.Log("Pacifist lost");
					attacked_last_turn = true;
				}
			}
		}
		if (callMentalType == MentalCaller.CallMentalType.TurnStart)
		{
			if (!attacked_last_turn)
			{
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

				foreach (GameObject playerOBJ in players)
				{
					HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
					if (grid_player.turn && grid_player.thisHeroStats.spearman)
					{
						grid_player.thisHeroStats.hero_current_HP += 5;
						GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay(grid_player.thisHeroStats);
					}
				}
			}
		}

		if (callMentalType == MentalCaller.CallMentalType.PathPointPassed)
		{
			attacked_last_turn = false;
		}
	}
}
