using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LvlUPButton : MonoBehaviour
{
	public TextMeshProUGUI stat_level_display;
	public Attribute_Type stat;


	private void Awake()
	{
		switch (stat)
		{
			case Attribute_Type.HP:
				stat_level_display.text = StatHolder.hp_lvl.ToString();
				break;
			case Attribute_Type.AP:
				stat_level_display.text = StatHolder.ap_lvl.ToString();
				break;
			case Attribute_Type.DEF:
				stat_level_display.text = StatHolder.def_lvl.ToString();
				break;
			case Attribute_Type.DMG:
				stat_level_display.text = StatHolder.dmg_lvl.ToString();
				break;
			case Attribute_Type.INT:
				stat_level_display.text = StatHolder.int_lvl.ToString();
				break;
			case Attribute_Type.JUMP:
				break;
			default:
				break;
		}
	}

	public void UpgradeStat()
	{
		switch (stat)
		{
			case Attribute_Type.HP:
				StatHolder.hp_lvl++;
				//GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[0].value.BaseValue += 20;
				StatHolder.spearman_max_HP += 20;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[0].UpdateStatDisplay();
				break;
			case Attribute_Type.AP:
				StatHolder.ap_lvl++;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[1].value.BaseValue += 1;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[1].UpdateStatDisplay();
				break;
			case Attribute_Type.DEF:
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[2].value.BaseValue += 10;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[2].UpdateStatDisplay();
				StatHolder.def_lvl++;
				break;
			case Attribute_Type.DMG:
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[3].value.BaseValue += 10;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[3].UpdateStatDisplay();
				StatHolder.dmg_lvl++;
				break;
			case Attribute_Type.INT:
				StatHolder.int_lvl++;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[4].value.BaseValue += 1;
				StatHolder.exp_multiplier += 0.5;
				GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[4].UpdateStatDisplay();
				break;
			case Attribute_Type.JUMP:
				break;
			default:
				break;
		}
	}
}
