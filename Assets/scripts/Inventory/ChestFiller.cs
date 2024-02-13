using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChestFiller : MonoBehaviour
{
    public int chest_quality;
    public UserIterface chest_items_holder;
    public GameObject takeAllButton;
    public inventoryObj[] chest_layouts_tier_1;
    public inventoryObj[] chest_layouts_tier_2;
    public inventoryObj[] chest_layouts_tier_3;
    void Awake()
    {
        /*
        if (journey_mngr.chhest_instantiated)
        {
            GameObject.Destroy(this.gameObject);
        }
        journey_mngr.chhest_instantiated = true;
        */
        DontDestroyOnLoad(this);
        SceneManager.sceneUnloaded += OnSceneUnoaded;
        inventoryObj chest_items = GameObject.FindGameObjectWithTag("chest").GetComponent<UserIterface>().inventory;

        ChestHolderOBJ chest = GameObject.FindGameObjectWithTag("chest_holder_OBJ").GetComponent<ChestHolderOBJ>();
        chest.chestFiller = this;

        chest_items.Clear();
        
        FillChest(chest_quality);

        SetVisable(false);
        //gameObject.SetActive(false);
    }

    void OnSceneUnoaded(Scene current)
    {
        //Destroy(this.gameObject);
        /*
        if (Game_Mode_Definer.Gamemode_Explore())
        {
            gameObject.SetActive(true);
            ChestHolderOBJ chest = GameObject.FindGameObjectWithTag("chest_holder_OBJ").GetComponent<ChestHolderOBJ>();
            chest.chestFiller = this;
            gameObject.SetActive(false);
            FillChest(chest_quality);
        }
        */
    }
    public void FillChest(int chest_quality)
    {
        
		switch (chest_quality)
		{
            case 1:
                int random_chest = Random.Range(0, chest_layouts_tier_1.Length);

                //chest_items_holder.inventory.container.slots = chest_layouts_tier_1[random_chest].container.slots;
                chest_items_holder.inventory.container.Clear();
                Debug.Log("Picked chest " + chest_layouts_tier_1[random_chest].name);
                foreach (InventorySlot slot in chest_layouts_tier_1[random_chest].GetSlots)
                {
                    if (slot.item.id > -1 && chest_items_holder.inventory.AddItem(slot.item, slot.ammount) && chest_items_holder.inventory.type == InterfaceType.Chest)
                    {
                        Debug.Log("Added to chest" + slot.item.name);
                    }
                }
                chest_items_holder.UpdateSlots();
                break;
            case 2:
                break;
            case 3:
                break;
			default:
                break;
        }
    }

    public void TakeAllItems()
    {
        inventoryObj player_inventory = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>().player_inventory;

        foreach (InventorySlot slot in chest_items_holder.inventory.GetSlots)
        {
            if (slot.item.id > -1)
            {
                player_inventory.AddItem(slot.item, slot.ammount);
                slot.RemoveItem();
            }
        }

    }

    public void SetVisable(bool visable)
    {
        if (!visable)
        {
            chest_items_holder.gameObject.SetActive(false);
            takeAllButton.SetActive(false);
        }
        else
        {
            chest_items_holder.gameObject.SetActive(true);
            takeAllButton.SetActive(true);
        }
    }
	private void OnApplicationQuit()
	{
        chest_items_holder.inventory.Clear();
	}
}
