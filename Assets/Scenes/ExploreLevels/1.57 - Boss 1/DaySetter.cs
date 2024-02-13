using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySetter : MonoBehaviour
{
	public int previous_day_to_set;
	private void Awake()
	{
		SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
	}

	private void SceneManager_sceneUnloaded(Scene arg0)
	{
		StoryProgression.day_count = previous_day_to_set;
	}
}
