using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownShot : MonoBehaviour
{
	bool in_shot_zone = false;
	float time_stayed = 0;
	public float stay_time = 1;
	public int deatlt_damage = 25;
	private void Update()
	{
		if (in_shot_zone)
		{
			time_stayed += Time.deltaTime;
			if (time_stayed >= stay_time)
			{
				StatHolder.spearman_current_HP -= deatlt_damage;
				GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
				StatHolder.CheckHealth();
				time_stayed = 0;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		in_shot_zone = true;
	}

	private void OnTriggerExit(Collider other)
	{
		in_shot_zone = false;
		time_stayed = 0;
	}
}
