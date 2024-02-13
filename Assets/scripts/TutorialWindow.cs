using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialWindow : MonoBehaviour
{
	public GameObject this_window;
	public GameObject this_arrow;
	public string arrow_name;
	public bool battle_tutorial;
	public Button continue_button;
	public HeroGR_Mov_Att hero;
	public bool destroyed_this_turn = true;
	public bool disables_end_turn = true;

	bool hero_turn_ended = false;
	private void Awake()
	{
		if (arrow_name != "")
		{
			foreach (GameObject arrow in GameObject.FindGameObjectWithTag("battle_tutorial").GetComponent<BattleTutorial>().tutorial_arrows)
			{
				if (arrow != null && arrow.name == arrow_name)
				{
					arrow.SetActive(true);
					this_arrow = arrow;
				}
			}
		}

		hero = null;
		SceneManager.activeSceneChanged += ChangedActiveScene;
		if (!battle_tutorial)
		{
			Invoke("PauseGame", 0.1f);
		}

		if (battle_tutorial)
		{
			if (disables_end_turn)
				GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = false;
			else
				GameObject.Find("EndTurnButton").GetComponent<Button>().interactable = true;
		}

	}

	private void Update()
	{
		if (hero != null)
		{
			if (!hero.turn)
				hero_turn_ended = true;
			if (hero_turn_ended)
			{
				if (hero.turn)
				{
					GameObject tutorial = GameObject.FindGameObjectWithTag("battle_tutorial");
					if (tutorial != null)
					{
						tutorial.GetComponent<BattleTutorial>().ChainWindowSpawn();
						Destroy(GameObject.FindGameObjectWithTag("tutorial_arrow"));
						Destroy(gameObject);
					}
				}
			}
		}


	}
	public void DestroyWindow()
	{
		if (battle_tutorial)
		{
			hero = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroGR_Mov_Att>();

			if (destroyed_this_turn)
			{
				GameObject tutorial = GameObject.FindGameObjectWithTag("battle_tutorial");
				if (tutorial != null)
				{
					tutorial.GetComponent<BattleTutorial>().ChainWindowSpawn();
					Destroy(GameObject.FindGameObjectWithTag("tutorial_arrow"));

					if (this_arrow != null)
						Destroy(this_arrow);
					Destroy(gameObject);
				}
			}

		}
		else
		{
			Time.timeScale = 1f;
			pause_menu.game_paused = false;
			Destroy(GameObject.FindGameObjectWithTag("tutorial_arrow"));
			Destroy(gameObject);
		}

	}

	private void ChangedActiveScene(Scene current, Scene next)
	{
		if (battle_tutorial)
		{
			GameObject tutorial = GameObject.FindGameObjectWithTag("battle_tutorial");
			if (tutorial == null)
				Destroy(this_window);
		}
	}

	void PauseGame()
	{
		Time.timeScale = 0f;
		pause_menu.game_paused = true;
	}
}
