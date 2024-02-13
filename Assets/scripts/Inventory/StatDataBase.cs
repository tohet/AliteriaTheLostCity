using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Batabase", menuName = "Stat Databse")]
[System.Serializable]
public class StatDataBase : ScriptableObject
{
	public Attribute hero_HP = new Attribute();
	public Attribute hero_AP = new Attribute();
	public Attribute hero_DEF = new Attribute();
	public Attribute hero_DMG = new Attribute();
	public Attribute hero_INT = new Attribute();
	public Attribute hero_JUMP = new Attribute();

	/*
	public void Clear()
	{
		spearman_HP.value.BaseValue = 100;
		spearman_AP.value.BaseValue = 4;
		spearman_DEF.value.BaseValue = 0;
		spearman_DEF.value.BaseValue = 0;
		spearman_INT.value.BaseValue = 3;
		spearman_JUMP.value.BaseValue = 2;
	}
	*/
}

