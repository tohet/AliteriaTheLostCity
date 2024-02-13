using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRange : ICustomDialogueScript
{
	public Usable_Object hinie_terget_range;
	void Start()
	{
		if (StoryProgression.target_range_passed)
			hinie_terget_range.dialogue_line_index = 9;
	}
	public override void RunFromDialogue()
	{
		InventoryHolder inventoryHolder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();

		foreach (Attribute attribute in inventoryHolder.hinie_attributes)
		{
			if (attribute.type == Attribute_Type.DMG)
			{
				attribute.value.BaseValue += 25;
			}
		}

		StoryProgression.target_range_passed = true;
	}
}
