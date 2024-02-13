using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MentalAct : ScriptableObject
{
	public virtual void DoMental(bool mode_is_explore) { } //true = explore, false = battle
	public virtual void DoMental(bool mode_is_explore, MentalCaller.CallMentalType callMentalType) { }
	public bool is_used_in_explore;
	public bool is_used_in_battle;
	public bool mental_applied;
	public virtual bool IsExplore { get { return is_used_in_explore; } set { } }
	public virtual bool IsBattle { get { return is_used_in_battle; } set { } }
}
