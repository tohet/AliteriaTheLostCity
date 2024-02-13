using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MChFiller : MonoBehaviour
{
	public inventoryObj choose_slots;
	public int next_line_index;
	public Dialogue_Mngr dialogue_manager;
	public UserIterface mental_choises;
	public DL_SO mental_dialogues;

	public GameObject skip_button;

	int previous_mental_ID;

	public void Start()
	{
		if (!StoryProgression.has_self_controlled)
			skip_button.SetActive(false);
		dialogue_manager = GameObject.FindGameObjectWithTag("dialogue_menu").GetComponent<Dialogue_Mngr>();
		FillMentals();
	}
	public void FillMentals()
	{
		foreach (InventorySlot slot in choose_slots.GetSlots)
		{
			//slot.RemoveItem();
			slot.item = ChooseRandomMental().data;
			slot.ammount = 1;
		}
	}
	public void TakeMental(int mental_id)
	{
		CallDialogueWindow(mental_id);
		this.gameObject.SetActive(false);
	}

	public void CallDialogueWindow(int mental_id)
	{
		GameObject dialogue_UI = GameObject.FindGameObjectWithTag("dialogue_menu");
		GameObject ui_obj = GameObject.FindGameObjectWithTag("pause_menu");

		StatHolder.dialogue_window_called = true;
		GameObject new_dialogue_UI = Instantiate(dialogue_UI, ui_obj.transform);
		new_dialogue_UI.transform.position = new Vector2(1446.1f, 500f);
		new_dialogue_UI.SetActive(true);
		new_dialogue_UI.GetComponent<Dialogue_Mngr>().used_object = this.gameObject;
		new_dialogue_UI.GetComponent<Dialogue_Mngr>().objectUsed = true;

		if (SceneManager.GetActiveScene().name == "DayEnd_Desert" && mental_id >= 0)
		{
			ReturnMentalDialogue(new_dialogue_UI.GetComponent<Dialogue_Mngr>(), mental_id);
		}
		else
			new_dialogue_UI.GetComponent<Dialogue_Mngr>().dialogue_line_index = next_line_index;
	}

	ItemObject ChooseRandomMental()
	{
		int rnd_MO_id;
		do
			rnd_MO_id = Random.Range(0, choose_slots.mentalDataBase.itemsObjects.Length);
		while (rnd_MO_id == previous_mental_ID);
		previous_mental_ID = rnd_MO_id;
		return choose_slots.mentalDataBase.itemsObjects[rnd_MO_id]; //можно замеить на индекс ментали, чтобы появлялась только она
	}

	void ReturnMentalDialogue(Dialogue_Mngr dialogue_window, int mental_id)
	{
		DL_SO previous_dialogues = dialogue_window.dialogue_lines;
		dialogue_window.dialogue_lines = mental_dialogues;

		switch (mental_id)
		{
			case 0:
				dialogue_window.dialogue_line_index = 0;
				break;
			case 1:
				dialogue_window.dialogue_line_index = 2;
				break;
			case 2:
				dialogue_window.dialogue_line_index = 6;
				break;
			case 3:
				dialogue_window.dialogue_line_index = 12;
				break;
			case 4:
				dialogue_window.dialogue_line_index = 14;
				break;
			case 5:
				dialogue_window.dialogue_line_index = 17;
				break;
			case 6:
				dialogue_window.dialogue_line_index = 20;
				break;
			case 7:
				dialogue_window.dialogue_line_index = 23;
				break;
			case 8:
				dialogue_window.dialogue_line_index = 26;
				break;
			case 9:
				dialogue_window.dialogue_line_index = 31;
				break;
			case 10:
				dialogue_window.dialogue_line_index = 34;
				break;
			case 11:
				dialogue_window.dialogue_line_index = 37;
				break;
			case 12:
				dialogue_window.dialogue_line_index = 41;
				break;
			case 13:
				dialogue_window.dialogue_line_index = 45;
				break;
			case 14:
				dialogue_window.dialogue_line_index = 49;
				break;
			case 15:
				dialogue_window.dialogue_line_index = 53;
				break;
			case 16:
				dialogue_window.dialogue_line_index = 56;
				break;
			case 17:
				dialogue_window.dialogue_line_index = 58;
				break;
			case 18:
				dialogue_window.dialogue_line_index = 62;
				break;
			case 19:
				dialogue_window.dialogue_line_index = 63;
				break;
			default:
				dialogue_window.dialogue_lines = previous_dialogues;
				dialogue_window.dialogue_line_index = next_line_index;
				break;
		}
		
		if (StoryProgression.day_count == 2)
		{
			dialogue_window.dialogue_lines = previous_dialogues;
			dialogue_window.dialogue_line_index = next_line_index;

		}
		
	}
}
