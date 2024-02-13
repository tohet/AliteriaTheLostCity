using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestHolderOBJ : MonoBehaviour
{
	public ChestFiller chestFiller;
	bool chest_open = false;
	private void Start()
	{

		chestFiller = GameObject.FindGameObjectWithTag("chest_holder").GetComponent<ChestFiller>();
		//chestFiller.gameObject.SetActive(false);
	}
	public void Use()
	{
		chestFiller = GameObject.FindGameObjectWithTag("chest_holder").GetComponent<ChestFiller>();
		if (!chest_open)
		{
			chestFiller.SetVisable(true);
			//chestFiller.gameObject.SetActive(true);
			chest_open = true;
		}

		else
		{
			chestFiller.SetVisable(false);
			//chestFiller.gameObject.SetActive(false);
			chest_open = false;
		}

	}

	public void LeaveAreaQuit()
	{
		chestFiller.SetVisable(false);
		//chestFiller.gameObject.SetActive(false);
		chest_open = false;
	}
	public void OutlineON()
	{
		GetComponent<Outline>().enabled = true;
	}

	public void OutlineOFF()
	{
		GetComponent<Outline>().enabled = false;
	}
}
