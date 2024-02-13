using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start : MonoBehaviour
{
	public void StartGame()
	{
		SceneManager.LoadScene("1.1 - Get Up");
	}
}
