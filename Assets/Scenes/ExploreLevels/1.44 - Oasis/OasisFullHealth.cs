using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OasisFullHealth : ICustomDialogueScript
{
    InventoryHolder inventoryHolder;
	void Start()
    {
		StatHolder.hinie_current_HP = StatHolder.hinie_max_Hp;
		GameObject.FindGameObjectWithTag("hinie_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
    }
	public override void RunFromDialogue()
	{
		StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
		GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
	}
}
