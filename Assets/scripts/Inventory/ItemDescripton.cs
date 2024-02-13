using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemDescripton : MonoBehaviour
{
	public RawImage description_background;
	public TextMeshProUGUI item_name;
	public TextMeshProUGUI item_description;

	public void InitializeDescription(ItemObject described_item)
	{
		item_name.text = described_item.name;
		item_description.text = described_item.discription;
	}
}
