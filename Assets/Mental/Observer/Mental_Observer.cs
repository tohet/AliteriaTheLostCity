using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Observer", menuName = "Mental/Observer")]
public class Mental_Observer : MentalAct
{
	public override bool IsExplore { get => base.IsExplore = is_used_in_explore; set { } }
	public override bool IsBattle { get => base.IsBattle = is_used_in_battle; set { } }
	public override void DoMental(bool mode_is_explore) //true = explore, false = battle
	{
		if (mode_is_explore)
		{
			Debug.Log("Observer Used");/*
			GameObject[] character_sensors = GameObject.FindGameObjectsWithTag("character_sensor");

			foreach (GameObject sensor in character_sensors)
			{
				Transform sensor_transform = sensor.GetComponent<Transform>();

				float increase_factor = 1.5f;

				Vector3 scale_increase = new Vector3(sensor_transform.localScale.x * increase_factor, sensor_transform.localScale.y * increase_factor, sensor_transform.localScale.z * increase_factor);
				sensor_transform.localScale = scale_increase;
				
			}
			*/
		}

		else
		{
			Debug.Log("Observer Used");
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("NPC");

			foreach (GameObject enemy in enemies)
			{
				Hero_Stats npc_stats = enemy.GetComponent<Hero_Stats>();
				npc_stats.healthBar.gameObject.SetActive(true);
			}
		}
	}
}

