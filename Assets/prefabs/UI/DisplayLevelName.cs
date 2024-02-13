using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayLevelName : MonoBehaviour
{
	public GameObject button;
	private void Start()
	{
		this.GetComponent<TextMeshProUGUI>().text = button.GetComponent<button_index>().level_name;
	}
}
