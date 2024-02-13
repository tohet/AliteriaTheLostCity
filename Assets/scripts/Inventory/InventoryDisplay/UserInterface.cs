using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public abstract class UserIterface : MonoBehaviour
{

	public MouseItem mouse_item;

	public inventoryObj inventory;

	public ItemDescripton item_description_box;
	ItemDescripton temp_description;

	public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

	private void Start()
	{
		/*
		mouse_item = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().mouse_item;
		//inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_inventory;

		foreach (InventorySlot slot in inventory.GetSlots)
		{
			slot.parent_interface = this;
			
			slot.OnAfterUpdate += OnSlotUpdate;
			
		}

		CreateSlots();
		UpdateSlots();
		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
		*/
	}

	public void OnSlotUpdate(InventorySlot _slot)
	{
		if (_slot.item.id >= 0)
		{
			if (_slot.slot_display.transform.GetChild(0).GetComponentInChildren<Image>().sprite == null)
			{
				_slot.slot_display.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.ItemObject.ui_display;
				_slot.slot_display.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
				_slot.slot_display.GetComponentInChildren<TextMeshProUGUI>().text = _slot.ammount == 1 ? "" : _slot.ammount.ToString("n0");
			}


		}

		else
		{
			_slot.slot_display.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
			_slot.slot_display.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
			_slot.slot_display.GetComponentInChildren<TextMeshProUGUI>().text = "";

		}
	}

	/*
	private void Update()
	{
		UpdateSlots();
	}
	*/
	public void UpdateSlots()
	{
		foreach (KeyValuePair<GameObject, InventorySlot> _slot in slotsOnInterface)
		{
			if (_slot.Value.item.id >= 0 && _slot.Value.ammount >= 1)
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.Value.ItemObject.ui_display;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.ammount == 1 ? "" : _slot.Value.ammount.ToString("n0");
			}

			else
			{
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
				_slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
				_slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";

			}

			if (temp_description != null)
			{
				Destroy(temp_description.gameObject);
				temp_description = null;
			}
		}
	}
	public abstract void CreateSlots();
	/*
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
			*/

	public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
	{
		EventTrigger trigger = obj.GetComponent<EventTrigger>();
		var eventTrigger = new EventTrigger.Entry();
		eventTrigger.eventID = type;
		eventTrigger.callback.AddListener(action);
		trigger.triggers.Add(eventTrigger);
	}

	public void OnEnter(GameObject obj)
	{
		mouse_item.slotHoveredOver = obj;

		if (slotsOnInterface[obj].item.id <= -1)
			return;
		//Debug.Log("Hovered over slot");

		temp_description = Instantiate(item_description_box);

		temp_description.transform.SetParent(transform.parent);
		temp_description.transform.position = obj.transform.position;
		float desc_box_x = temp_description.transform.position.x;
		temp_description.transform.position = new Vector3(desc_box_x + 75, temp_description.transform.position.y);
		temp_description.InitializeDescription(slotsOnInterface[obj].ItemObject);

		mouse_item.temp_mouse_item_description = temp_description;
		/*
		if (slotsOnInterface.ContainsKey(obj))
		{
			mouse_item.hover_item = slotsOnInterface[obj];
		}
		*/
	}

	public void OnExit(GameObject obj)
	{
		mouse_item.slotHoveredOver = null;

		if (mouse_item.temp_mouse_item_description != null)
			Destroy(mouse_item.temp_mouse_item_description.gameObject);

		/*
		mouse_item.slotHoveredOver = null;
		mouse_item.hover_item = null;
		*/
	}

	public void OnDragStart(GameObject obj)
	{
		if (slotsOnInterface[obj].item.id <= -1)
			return;

		GameObject mouse_object = new GameObject();
		RectTransform rt = mouse_object.AddComponent<RectTransform>();
		rt.sizeDelta = new Vector2(50, 50);
		mouse_object.transform.SetParent(transform.parent);

		//проверяет по ID, есть ли такой предмет
		if (slotsOnInterface[obj].item.id >= 0)
		{
			Image img = mouse_object.AddComponent<Image>();
			img.sprite = slotsOnInterface[obj].ItemObject.ui_display;
			img.raycastTarget = false;
		}

		mouse_item.tempItemDragged = mouse_object;
		/*
		mouse_item.item = slotsOnInterface[obj];
		*/
	}


	public void OnDragEnd(GameObject obj)
	{
/*		
		InventorySlot mouseHoverItem = mouse_item.hover_item;
		GameObject mouseHoverObj = mouse_item.slotHoveredOver;
*/

		Destroy(mouse_item.tempItemDragged);

		if (mouse_item.interfaceMouseIsOver == null)
		{
			slotsOnInterface[obj].RemoveItem();
			return;
		}

		if (mouse_item.slotHoveredOver)
		{
			InventorySlot mouseHoveredSlotData = mouse_item.interfaceMouseIsOver.slotsOnInterface[mouse_item.slotHoveredOver];
			inventory.SwapItems(slotsOnInterface[obj], mouseHoveredSlotData);
		}

/*
		Dictionary<int, ItemObject> getItemOBj = inventory.dataBase.getItem;

		if (mouse_item.interfaceMouseIsOver != null)
		{
			if (mouseHoverObj)
			{
				if (mouseHoverItem.CanPlaceInSlot(getItemOBj[items_displayed[obj].id]) && (mouseHoverItem.item.id <= -1 || (mouseHoverItem.item.id >= 0 && items_displayed[obj].CanPlaceInSlot(getItemOBj[mouseHoverItem.item.id]))))
					inventory.MoveItem(items_displayed[obj], mouseHoverItem.parent_interface.items_displayed[mouse_item.slotHoveredOver]);
			}
		}
		else
		{
			inventory.RemoveItem(items_displayed[obj].item);
		}

		mouse_item.item = null;
*/
	}

	public void OnDrag(GameObject obj)
	{
		if (mouse_item.tempItemDragged != null)
		{
			mouse_item.tempItemDragged.GetComponent<RectTransform>().position = Input.mousePosition;
		}
	}

	public void OnEnterInterface(GameObject obj)
	{
		mouse_item.interfaceMouseIsOver = obj.GetComponent<UserIterface>();
	}

	public void OnExitInterface(GameObject obj)
	{
		mouse_item.interfaceMouseIsOver = null;
	}

	public void InitializeSlots()
	{
		mouse_item = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().mouse_item;
		//inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_inventory;

		foreach (InventorySlot slot in inventory.GetSlots)
		{
			slot.parent_interface = this;

			slot.OnAfterUpdate += OnSlotUpdate;

		}

		CreateSlots();
		UpdateSlots();
		AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
		AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
	}
}

public class MouseItem
{
	public UserIterface interfaceMouseIsOver;
	public GameObject tempItemDragged;
	public InventorySlot item;
	public InventorySlot hover_item;
	public GameObject slotHoveredOver;
	public ItemDescripton temp_mouse_item_description;
}

