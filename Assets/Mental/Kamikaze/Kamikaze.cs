using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kamikaze", menuName = "Mental/Kamikaze")]
public class Kamikaze : MentalAct
{
    public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
    public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.DamageTaken)
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
				if (grid_player.thisHeroStats.spearman && grid_player.thisHeroStats.hero_current_HP <= 0)
				{
					GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

					foreach (GameObject npc in npcs)
					{
						Hero_Stats npc_stats = npc.GetComponent<Hero_Stats>();
						npc_stats.hero_current_HP -= 100;
						npc_stats.healthBar.SetHealth(npc_stats.hero_current_HP);
					}
				}
			}

		}
	}
	/*
if (Input.GetKeyDown(KeyCode.Space))
{
    if (this.gameObject.tag == "NPC")
    {
        hero_current_HP -= 50;
        healthBar.SetHealth(hero_current_HP);
    }

}
*/
}
