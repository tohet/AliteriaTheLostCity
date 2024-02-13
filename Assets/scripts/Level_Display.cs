using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Display : MonoBehaviour
{
	public TextMeshProUGUI level_label;
	public TextMeshProUGUI current_exp;
	public TextMeshProUGUI needed_exp;
	private void OnEnable()
	{
		level_label.text = StatHolder.hero_level.ToString();
		current_exp.text = StatHolder.hero_EXP.ToString();
		needed_exp.text = StatHolder.exp_to_level_up.ToString();
	}
}
