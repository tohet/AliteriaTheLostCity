using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Panicking", menuName = "Mental/Panicking")]
public class Mental_Panicking : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.TurnStart)
		{
			GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

			foreach (GameObject playerOBJ in players)
			{
				if (playerOBJ.GetComponent<HeroGR_Mov_Att>().turn && playerOBJ.GetComponent<HeroGR_Mov_Att>().was_attacked_before_turn)
				{
					playerOBJ.GetComponent<HeroGR_Mov_Att>().move++;
				}
			}
		}
	}
}
