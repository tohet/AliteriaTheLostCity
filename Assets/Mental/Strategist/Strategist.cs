using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Strategist", menuName = "Mental/Strategist")]
public class Strategist : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }

	bool took_damage = false;
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.DamageDealt)
		{
			took_damage = true;
		}

		if (callMentalType == MentalCaller.CallMentalType.PathPointPassed)
		{
			if (!took_damage)
			{
				StatHolder.IncreaceEXP(System.Convert.ToInt32(300 * StatHolder.exp_multiplier));
			}
			took_damage = false;
		}
	}
}
