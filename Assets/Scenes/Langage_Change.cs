using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Langage_Change : MonoBehaviour
{
	public TMP_Dropdown language_options;
	private void Start()
	{
		language_options = this.gameObject.GetComponent<TMP_Dropdown>();
		/*
		dropdown.options.Clear();
		List<string> language_titles = new List<string>();
		language_titles.Add("English");
		language_titles.Add("Русский");

		foreach (var item in language_titles)
			dropdown.options.Add(new Dropdown.OptionData() { text = item });
		*/
		language_options.onValueChanged.AddListener(delegate { ChangeLanguage(); });
	}

	public void ChangeLanguage()
	{

		if (language_options.value > 0)
			StoryProgression.russian_language_picked = true;
		else
			StoryProgression.russian_language_picked = false;
	}
}
