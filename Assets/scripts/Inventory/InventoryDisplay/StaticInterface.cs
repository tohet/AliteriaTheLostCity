using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Linked to a specific database
public class StaticInterface : UserIterface
{
    // Start is called before the first frame update

    public GameObject[] slots;
    public override void CreateSlots() 
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

		for (int i = 0; i < inventory.GetSlots.Length; i++)
		{
            GameObject obj = slots[i];

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            inventory.GetSlots[i].slot_display = obj;

            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }
}
