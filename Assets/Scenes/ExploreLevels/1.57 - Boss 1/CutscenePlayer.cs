using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutscenePlayer : MonoBehaviour
{
	public string load_scene;
	public bool resets_day = false;
	private VideoPlayer cutscene;
	private void Awake()
	{
		cutscene = GetComponent<VideoPlayer>();
		cutscene.loopPointReached += LoadSceneAfterVideo;
	}

	void LoadSceneAfterVideo(VideoPlayer player)
	{
		if (load_scene != null)
		{
			if (resets_day)
			{
				GameObject.FindGameObjectWithTag("pause_menu").GetComponent<pause_menu>().ResetDay();
			}
			SceneManager.LoadScene(load_scene);
		}
	}
}
