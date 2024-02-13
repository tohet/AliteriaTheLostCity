using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTutWindow : MonoBehaviour
{
    public HeroGR_Mov_Att hero_grid;
    bool hero_turn_started = false;
    bool window_destroyed = false;
    void Update()
    {
        if (!hero_grid.turn && !hero_turn_started)
            return;
        hero_turn_started = true;

        if (!hero_grid.turn && !window_destroyed)
        {
            hero_grid.DestroyTutorialWindow();
            Destroy(GameObject.FindGameObjectWithTag("battle_tutorial"));
            window_destroyed = true;
        }
    }
}
