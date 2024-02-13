using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Rager", menuName = "Mental/Rager")]
public class Rager : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }
	public override void DoMental(bool mode_is_explore)
	{
		if (!mental_applied)
		{
			InventoryHolder modified_stats_holder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();

			for (int i = 0; i < modified_stats_holder.spearman_attributes.Length; i++)
			{

				if (modified_stats_holder.spearman_attributes[i].type == Attribute_Type.DMG)
				{
					modified_stats_holder.spearman_attributes[i].value.BaseValue += 20;
				}
			}

			mental_applied = true;
		}
	}
}
