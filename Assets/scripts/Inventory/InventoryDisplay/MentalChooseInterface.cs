using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MentalChooseInterface : UserIterface
{
    // Start is called before the first frame update
    public GameObject[] slots;

	private void Start()
	{
        InitializeSlots();
	}
	public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            GameObject obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            /*
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            */
            inventory.GetSlots[i].slot_display = obj;
            inventory.GetSlots[i].allowed_items = new ItemType[1] { ItemType.Mental };

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
}
