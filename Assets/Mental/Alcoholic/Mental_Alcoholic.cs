using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mental", menuName = "Mental/Alcoholic")]
public class Mental_Alcoholic : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }
	public override void DoMental(bool mode_is_explore)
	{
		if (!mental_applied)
		{
			StatHolder.use_potion_AP_cost -= 1;
			mental_applied = true;
		}
	}
}
