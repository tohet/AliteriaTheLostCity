using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class SceneLogic1 : MonoBehaviour
{
	private void Awake()
	{
		GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().hinie_health_display.gameObject.SetActive(true);
		SetHinieEquipment(true);
		GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().hinie_stats_display.SetActive(true);

		StoryProgression.day_count++;

		StatHolder.spearman_hit_in_knee = false;
		StatHolder.hinie_hit_in_knee = false;
		
		GameObject.Find("Day_Counter").GetComponent<TextMeshProUGUI>().text = StoryProgression.day_count.ToString();

		SceneManager.sceneUnloaded += OnSceneUnloaded;

		if (StoryProgression.day_count == 9)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 24;
			StatHolder.spearman_current_HP += StatHolder.spearman_max_HP / 2;
			StatHolder.hinie_current_HP += StatHolder.hinie_max_Hp / 2;

			if (StatHolder.spearman_current_HP > StatHolder.spearman_max_HP)
				StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
			if (StatHolder.hinie_current_HP > StatHolder.hinie_max_Hp)
				StatHolder.hinie_current_HP = StatHolder.hinie_max_Hp;
		}
			
		if (StoryProgression.first_night && !StoryProgression.met_hinie_before)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 2;
			StatHolder.hinie_current_HP = StatHolder.hinie_max_Hp;
			StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
			StoryProgression.first_night = false;
			StoryProgression.met_hinie_before = true;
		}

		else if (StoryProgression.met_hinie_before && StoryProgression.day_count == 2)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 19;
			StatHolder.hinie_current_HP = StatHolder.hinie_max_Hp;
			StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
			//StoryProgression.first_night = false;
		}
		else if (StoryProgression.day_count != 9)
		{
			gameObject.GetComponent<Usable_Object>().dialogue_line_index = 21;
			StatHolder.spearman_current_HP += StatHolder.spearman_max_HP / 2;
			StatHolder.hinie_current_HP += StatHolder.hinie_max_Hp / 2;

			if (StatHolder.spearman_current_HP > StatHolder.spearman_max_HP)
				StatHolder.spearman_current_HP = StatHolder.spearman_max_HP;
			if (StatHolder.hinie_current_HP > StatHolder.hinie_max_Hp)
				StatHolder.hinie_current_HP = StatHolder.hinie_max_Hp;
		}

	}

	void OnSceneUnloaded(Scene current)
	{
		/*
		GameObject[] chest_holders = GameObject.FindGameObjectsWithTag("chest_holder");
		foreach (GameObject chest in chest_holders)
			Destroy(chest.gameObject);
		*/
	}

	void SetHinieEquipment(bool visable)
	{
		if (visable)
		{
			StaticInterface equipment_interface = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().equipment_interface;
			equipment_interface.slots[0].gameObject.SetActive(true);
			equipment_interface.slots[4].gameObject.SetActive(true);
		}
	}
}
