using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverWorldManager : MonoBehaviour
{
    static int wins = 0;

    void Update()
    {
        if (wins >= 4)
        {
            SceneManager.LoadScene("outro");
        }
    }

    public static void IncreaseWins()
    {
        wins++;
    }
}
