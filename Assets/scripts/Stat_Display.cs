using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Stat_Display : MonoBehaviour
{
    public TextMeshProUGUI stat_display;
    public TextMeshProUGUI max_hp_display;
    public bool spearman;
    public bool hihie;

    public Attribute_Type attribute;
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        ChangeStatDisplay();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeStatDisplay();
    }

    public void ChangeStatDisplay(Hero_Stats _player)
    {
        stat_display.text = _player.hero_current_HP.ToString();
    }

    public void ChangeStatDisplay()
    {
        InventoryHolder inventoryHolder = GameObject.FindGameObjectWithTag("inventory_holder").GetComponent<InventoryHolder>();
        switch (attribute)
		{
			case Attribute_Type.HP:
                if (spearman)
                {
                    stat_display.text = StatHolder.spearman_current_HP.ToString("n0");
                    max_hp_display.text = StatHolder.spearman_max_HP.ToString("n0");
                }

                else if (hihie)
                {
                    stat_display.text = StatHolder.hinie_current_HP.ToString("n0");
                    max_hp_display.text = StatHolder.hinie_max_Hp.ToString("n0");
                }
                break;
			case Attribute_Type.AP:
                if (spearman)
                {
                    foreach (Attribute stat in inventoryHolder.spearman_attributes)
                    {
                        if (stat.type == Attribute_Type.AP)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }

                else if (hihie)
                {
                    foreach (Attribute stat in inventoryHolder.hinie_attributes)
                    {
                        if (stat.type == Attribute_Type.AP)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }
                break;
			case Attribute_Type.DEF:
                if (spearman)
                {
                    foreach (Attribute stat in inventoryHolder.spearman_attributes)
                    {
                        if (stat.type == Attribute_Type.DEF)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }

                else if (hihie)
                {
                    foreach (Attribute stat in inventoryHolder.hinie_attributes)
                    {
                        if (stat.type == Attribute_Type.DEF)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }
                break;
			case Attribute_Type.DMG:
                if (spearman)
                {
                    foreach (Attribute stat in inventoryHolder.spearman_attributes)
                    {
                        if (stat.type == Attribute_Type.DMG)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }

                else if (hihie)
                {
                    foreach (Attribute stat in inventoryHolder.hinie_attributes)
                    {
                        if (stat.type == Attribute_Type.DMG)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }
                break;
			case Attribute_Type.INT:
                if (spearman)
                {
                    foreach (Attribute stat in inventoryHolder.spearman_attributes)
                    {
                        if (stat.type == Attribute_Type.INT)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }

                else if (hihie)
                {
                    foreach (Attribute stat in inventoryHolder.hinie_attributes)
                    {
                        if (stat.type == Attribute_Type.INT)
                        {
                            stat_display.text = stat.value.ModifiedValue.ToString();
                        }
                    }
                }
                break;
			case Attribute_Type.JUMP:
                if (spearman)
                {

                }

                else if (hihie)
                {

                }
                break;
			default:
				break;
		}

    }
}
