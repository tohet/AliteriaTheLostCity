using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneTextureDestroyer : MonoBehaviour
{
	GameObject this_object;
	private void Awake()
	{
		this_object = gameObject;
		//SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
		SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
	}

	private void SceneManager_activeSceneChanged(Scene arg0, Scene arg1)
	{
		Destroy(this_object);
	}
}
