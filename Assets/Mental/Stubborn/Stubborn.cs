using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stubborn", menuName = "Mental/Stubborn")]
public class Stubborn : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	bool revived = false;
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.DamageTaken)
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (grid_player.thisHeroStats.spearman)
				{
					if (grid_player.thisHeroStats.hero_current_HP <= 0 && !revived)
					{
						Debug.Log("Restored hero with 10 hP");
						grid_player.thisHeroStats.hero_current_HP = 10;
						GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay(grid_player.thisHeroStats);
						revived = true;
					}
				}
			}
		}

		if (callMentalType == MentalCaller.CallMentalType.PathPointPassed)
		{
			Debug.Log("Stubborn revive reset");
			revived = false;
		}
	}
}
