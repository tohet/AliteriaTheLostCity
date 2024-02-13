using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ArmorDisplay : MonoBehaviour
{
	public TextMeshProUGUI armor_label;
	public GameObject armor_icon;
	public bool spearman;
	private void Awake()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		UpdateArmorDisplay();
	}

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
		UpdateArmorDisplay();
    }

	public void UpdateArmorDisplay()
	{
		if (!Game_Mode_Definer.Gamemode_Explore())
		{
			armor_label.gameObject.SetActive(true);
			GameObject[] grid_players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in grid_players)
			{
				if (player.GetComponent<HeroGR_Mov_Att>().thisHeroStats.spearman && spearman)
					armor_label.text = player.GetComponent<HeroGR_Mov_Att>().thisHeroStats.hero_DEF.ToString();
				else if (player.GetComponent<HeroGR_Mov_Att>().thisHeroStats.hinie && !spearman)
					armor_label.text = player.GetComponent<HeroGR_Mov_Att>().thisHeroStats.hero_DEF.ToString();
			}
			/*
			if (spearman)
				armor_label.text = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().spearman_attributes[2].value.ModifiedValue.ToString();
			else
				armor_label.text = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().hinie_attributes[2].value.ModifiedValue.ToString();
			*/
			armor_icon.SetActive(true);
		}

		else
		{
			armor_label.gameObject.SetActive(false);
			armor_icon.SetActive(false);
		}
	}
}
