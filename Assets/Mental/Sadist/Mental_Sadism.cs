using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mental", menuName = "Mental/Sadism")]
public class Mental_Sadism : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	public override void DoMental(bool mode_is_explore) //true = explore, false = battle
	{
		
		if (!mental_applied)
		{
			StatHolder.spearman_max_HP -= 10;
			if (StatHolder.spearman_current_HP > StatHolder.spearman_max_HP)
				StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
			mental_applied = true;

			InventoryHolder modified_stats_holder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();

			for (int i = 0; i < modified_stats_holder.spearman_attributes.Length; i++)
			{
				if (modified_stats_holder.spearman_attributes[i].type == Attribute_Type.HP)
				{
					modified_stats_holder.spearman_attributes[i].value.BaseValue -= 10;
					if (StatHolder.spearman_current_HP > modified_stats_holder.spearman_attributes[i].value.BaseValue)
						StatHolder.spearman_current_HP = modified_stats_holder.spearman_attributes[i].value.BaseValue;
				}
			}
		}

	}

	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.DamageDealt)
		{
			
			//GameObject.FindGameObjectWithTag("Player").GetComponent<Hero_Stats>().hero_current_HP += 5;

			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (grid_player.turn && !(grid_player.isMoving) && grid_player.thisHeroStats.spearman)
				{
					grid_player.thisHeroStats.hero_current_HP += 10;
					GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay(grid_player.thisHeroStats);
				}
			}
		}
	}
}
