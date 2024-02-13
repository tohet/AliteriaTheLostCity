using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseButton : MonoBehaviour
{
	public StaticInterface usables_display;
	public inventoryObj usables_inventory;
	public int slot_index;
	public void UseItem()
	{
		
		GameObject player = GameObject.FindGameObjectWithTag("Player");

		if (player.GetComponent<Hero>() != null)
		{
			//playmode is EXPLORE
			if (HasItemToUse(usables_inventory.GetSlots[slot_index]))
			{
				Debug.Log("Reached a thing!");

				InventorySlot slot = usables_inventory.GetSlots[slot_index];

				switch (slot.ItemObject.type)
				{
					case ItemType.Default:
						break;
					case ItemType.HealthPotion:

						if (slot.ItemObject.heal_value == 0 && slot.ItemObject.resote_AP_value == 0)
						{
							StatHolder.spearman_armor_bonus += 30;
							StatHolder.hinie_armor_bonus += 30;
						}

						else
						{
							StatHolder.spearman_current_HP += slot.ItemObject.heal_value / 2;
							StatHolder.hinie_current_HP += slot.ItemObject.heal_value / 2;

							GameObject.FindGameObjectWithTag("spearman_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
							if (StoryProgression.day_count > 1)
								GameObject.FindGameObjectWithTag("hinie_health_display").GetComponent<Stat_Display>().ChangeStatDisplay();
							//добавить код для Хини

							if (slot.ItemObject.resote_AP_value > 2)
								player.GetComponent<Hero>().speed_multiplier += slot.ItemObject.resote_AP_value - 2;
						}

						slot.AddAmmount(-1);

						if (slot.ammount <= 0)
						{
							slot.RemoveItem();
						}
						break;
					case ItemType.Map:
						break;
					case ItemType.Key:
						break;
					case ItemType.Helmet:
						break;
					case ItemType.Chestplate:
						break;
					case ItemType.Gloves:
						break;
					case ItemType.Boots:
						break;
					case ItemType.Spear:
						break;
					case ItemType.Bow:
						break;
					case ItemType.Trinket:
						break;
					case ItemType.Mental:
						break;
					default:
						break;
				}
				usables_display.UpdateSlots();
			}
		}

		else
		{
			//playmode is BATTLE
			if (HasItemToUse(usables_inventory.GetSlots[slot_index]))
			{
				Debug.Log("Reached a thing!");
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

				foreach (GameObject playerOBJ in players)
				{
					HeroGR_Mov_Att grid_player = playerOBJ.GetComponent<HeroGR_Mov_Att>();
					if (grid_player.move >= 2 && grid_player.turn && !(grid_player.isMoving))
					{
						Debug.Log("Used a thing!");
						//grid_player.UseHealthPotion();
						
						InventorySlot slot = usables_inventory.GetSlots[slot_index];

						switch (slot.ItemObject.type)
						{
							case ItemType.Default:
								break;
							case ItemType.HealthPotion:
								grid_player.move -= StatHolder.use_potion_AP_cost;

								if (slot.ItemObject.heal_value == 0 && slot.ItemObject.resote_AP_value == 0)
								{
									if (grid_player.thisHeroStats.spearman)
									{
										StatHolder.spearman_armor_bonus += 60;
										grid_player.thisHeroStats.hero_DEF += 60;
										GameObject[] armorDisplays = GameObject.FindGameObjectsWithTag("armor_display");
										foreach (GameObject armor_display in armorDisplays)
										{
											if (armor_display.GetComponent<ArmorDisplay>().spearman)
												armor_display.GetComponent<ArmorDisplay>().UpdateArmorDisplay();
										}
									}
									else
									{
										StatHolder.hinie_armor_bonus += 60;
										grid_player.thisHeroStats.hero_DEF += 60;
										GameObject[] armorDisplays = GameObject.FindGameObjectsWithTag("armor_display");
										foreach (GameObject armor_display in armorDisplays)
										{
											if (!armor_display.GetComponent<ArmorDisplay>().spearman)
												armor_display.GetComponent<ArmorDisplay>().UpdateArmorDisplay();
										}
									}
								}

								grid_player.thisHeroStats.hero_current_HP += slot.ItemObject.heal_value;
								grid_player.thisHeroStats.health_display.ChangeStatDisplay(grid_player.thisHeroStats);
								grid_player.move += slot.ItemObject.resote_AP_value;

								slot.AddAmmount(-1);

								if (slot.ammount <= 0)
								{
									slot.RemoveItem();
								}

								if (grid_player.move <= 0)
								{
									//turnManager.EndTurn();
									TurnManager turnManager = GameObject.FindGameObjectWithTag("turn_manager").GetComponent<TurnManager>();
									turnManager.Invoke("EndTurn", 0.7f);
								}
								break;
							case ItemType.Map:
								break;
							case ItemType.Key:
								break;
							case ItemType.Helmet:
								break;
							case ItemType.Chestplate:
								break;
							case ItemType.Gloves:
								break;
							case ItemType.Boots:
								break;
							case ItemType.Spear:
								break;
							case ItemType.Bow:
								break;
							case ItemType.Trinket:
								break;
							case ItemType.Mental:
								break;
							default:
								break;
						}
						usables_display.UpdateSlots();
					}
				}
			}
		}
	}

	bool HasItemToUse(InventorySlot slot)
	{
		if (slot.item != null && slot.ammount > 0)
		{
			return true;
		}
		return false;
	}

	void GetItemTypeAndUse()
	{

	}
}
