using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Explorer", menuName = "Mental/Explorer")]
public class Explorer : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.PathPointPassed)
		{
			StatHolder.IncreaceEXP(System.Convert.ToInt32(10 * StatHolder.exp_multiplier));
		}
	}
}
