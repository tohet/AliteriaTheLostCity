using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//The interface can be changed

public class DynamicInterface : UserIterface
{
	public GameObject inventory_prefab;

	public int x_start;
	public int y_start;

	public int x_space_between_item;
	public int y_space_between_item;

	public int collums;
	public override void CreateSlots()
	{
		/*
		for (int i = 0; i < inventory.container.items.Count; i++)
		{
			InventorySlot slot = inventory.container.items[i];
			GameObject obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
			obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[slot.item.id].ui_display;
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
			obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.ammount.ToString("n0");
			items_displayed.Add(slot, obj);
		}
		*/

		slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
			GameObject obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

			AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
			AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
			AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
			AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
			AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
			inventory.GetSlots[i].slot_display = obj;

			
			slotsOnInterface.Add(obj, inventory.GetSlots[i]);
		}
	}

	public Vector3 GetPosition(int i)
	{
		return new Vector3(x_start + (x_space_between_item * (i % collums)), y_start + (-y_space_between_item * (i / collums)), 0f);
	}

}
