using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Adventurer", menuName = "Mental/Adventurer")]
public class Mental_Adventurer : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }
	public override void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType)
	{
		if (callMentalType == MentalCaller.CallMentalType.PathPointPassed)
		{
			if (StatHolder.spearman_current_HP < StatHolder.spearman_max_HP)
			{
				Debug.Log("Adventurer gave 20 HP");
				StatHolder.spearman_current_HP += 20;
				GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
			}
		}
	}
}
