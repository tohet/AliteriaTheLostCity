using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorelatedArrow : MonoBehaviour
{
	public GameObject corelated_arrow;
	private void Awake()
	{
		corelated_arrow.SetActive(true);
	}

	private void OnDestroy()
	{
		Destroy(corelated_arrow);
	}
}
