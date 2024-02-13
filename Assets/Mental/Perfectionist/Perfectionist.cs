using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Perfectionist", menuName = "Mental/Perfectionist")]
public class Perfectionist : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	bool bonus_DMG_applied = false;
	bool bonus_lost = false;
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.TurnStart)
		{
			//increase hero DMG
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (grid_player.thisHeroStats.spearman && !bonus_DMG_applied)
				{
					grid_player.thisHeroStats.heroDamage += 20;
					bonus_DMG_applied = true;
				}
			}
		}

		if (callMentalType == MentalCaller.CallMentalType.DamageTaken)
		{
			//decrase hero DMG
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (grid_player.thisHeroStats.spearman && !bonus_lost)
				{
					grid_player.thisHeroStats.heroDamage -= 30;
					bonus_lost = true;
				}
			}
		}

		if (callMentalType == MentalCaller.CallMentalType.PathPointPassed)
		{
			bonus_DMG_applied = false;
			bonus_lost = false;
		}
	}
}
