using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHideHinieHealth : MonoBehaviour
{
    void Start()
    {
        GameObject.FindGameObjectWithTag("hinie_health_display").SetActive(false);
    }

}
