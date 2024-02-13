using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayHeldItems : MonoBehaviour
{
	/*
	public MouseItem mouse_item = new MouseItem();

	public GameObject inventory_prefab;
	public inventoryObj inventory;

	Dictionary<GameObject, InventorySlot> items_displayed = new Dictionary<GameObject, InventorySlot>();
	
	private void Start()
	{
		inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_inventory;
		CreateSlots();
	}

	private void Update()
	{
		UpdateSlots();
	}

	public void UpdateSlots()
	{
		foreach (KeyValuePair<GameObject, InventorySlot> _slot in items_displayed)
		{
			if (_slot.Value.id >= 0)
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[_slot.Value.item.id].ui_display;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.ammount == 1 ? "" : _slot.Value.ammount.ToString("n0");
			}

			else
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";

			}
		}
	}
	
	public void CreateSlots()
	{
		
		for (int i = 0; i < inventory.container.items.Count; i++)
		{
			InventorySlot slot = inventory.container.items[i];
			GameObject obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
			obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[slot.item.id].ui_display;
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
			obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.ammount.ToString("n0");
			items_displayed.Add(slot, obj);
		}
		

		items_displayed = new Dictionary<GameObject, InventorySlot>();

		for (int i = 0; i < inventory.container.items.Length; i++)
		{
			GameObject obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
			obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

			AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
			AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
			AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
			AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
			AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

			items_displayed.Add(obj, inventory.container.items[i]);
		}
	}

	
	public void UpdateDisplay()
	{
		for (int i = 0; i < inventory.container.items.Count; i++)
		{
			InventorySlot slot = inventory.container.items[i];

			if (items_displayed.ContainsKey(slot))
			{
				items_displayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.ammount.ToString("n0");
			}

			else
			{
				GameObject obj = Instantiate(inventory_prefab, Vector3.zero, Quaternion.identity, transform);
				obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[slot.item.id].ui_display;
				obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
				obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.ammount.ToString("n0");
				items_displayed.Add(slot, obj);

			}
		}


	}


	private void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = obj.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}

	public void OnEnter(GameObject obj)
	{
		mouse_item.hover_object = obj;
		if (items_displayed.ContainsKey(obj))
		{
			mouse_item.hover_item = items_displayed[obj];
		}
	}

	public void OnExit(GameObject obj)
	{
		mouse_item.hover_object = null;
		mouse_item.hover_item = null;
	}

	public void OnDragStart(GameObject obj)
	{
		GameObject mouse_object = new GameObject();
		RectTransform rt = mouse_object.AddComponent<RectTransform>();
		rt.sizeDelta = new Vector2(50, 50);
		mouse_object.transform.SetParent(transform.parent);

		//проверяет по ID, есть ли такой предмет
		if (items_displayed[obj].id >= 0)
		{
			Image img = mouse_object.AddComponent<Image>();
			img.sprite = inventory.dataBase.getItem[items_displayed[obj].id].ui_display;
			img.raycastTarget = false;
		}

		mouse_item.obj = mouse_object;
		mouse_item.item = items_displayed[obj];
	}

	public void OnDragEnd(GameObject obj)
	{
		if (mouse_item.hover_object)
		{
			inventory.MoveItem(items_displayed[obj], items_displayed[mouse_item.hover_object]);
		}

		else
		{
			inventory.RemoveItem(items_displayed[obj].item);
		}

		Destroy(mouse_item.obj);
		mouse_item.item = null;
	}

	public void OnDrag(GameObject obj)
	{
		if (mouse_item.obj != null)
		{
			mouse_item.obj.GetComponent<RectTransform>().position = Input.mousePosition;
		}
	}
	*/
}



