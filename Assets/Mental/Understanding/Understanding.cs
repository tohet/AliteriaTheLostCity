using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Understanding", menuName = "Mental/Understanding")]
public class Understanding : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }
	public override void DoMental(bool mode_is_explore)
	{
		if (!mental_applied)
		{
			StatHolder.IncreaceEXP(StatHolder.exp_to_level_up);
			mental_applied = true;
		}
	}
}
