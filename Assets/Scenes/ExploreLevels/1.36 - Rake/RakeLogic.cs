using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeLogic : ICustomDialogueScript
{
	public Usable_Object rake_usable;
	void Start()
    {
		if (StoryProgression.rake_hits >= 10 && !StoryProgression.rake_comprehended)
		{
			StoryProgression.rake_comprehended = true;

			rake_usable.dialogue_line_index = 6;

			StatHolder.int_lvl++;
			GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[4].value.BaseValue += 1;
			StatHolder.exp_multiplier += 0.5;
			GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[4].UpdateStatDisplay();
		}
    }

	public override void RunFromDialogue()
	{
		StoryProgression.rake_hits++;
		StatHolder.IncreaceEXP(System.Convert.ToInt32(50 * StatHolder.exp_multiplier));
	}

}
