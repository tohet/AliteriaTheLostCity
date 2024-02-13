using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_iconContr : MonoBehaviour
{
    public HeroGR_Mov_Att hero1;
    public HeroGR_Mov_Att hero2;
    public GameObject move_icon;
    public GameObject attack_icon;


    void Update()
    {
        if (hero2 != null)
        {
            if (hero1.turn || hero2.turn)

            {
                if (hero1.modeIsAttack || hero2.modeIsAttack)
                {
                    move_icon.SetActive(false);
                    attack_icon.SetActive(true);
                }

                else if (!hero1.modeIsAttack && !hero2.modeIsAttack)
                {
                    attack_icon.SetActive(false);
                    move_icon.SetActive(true);
                }

            }

            else
            {
                move_icon.SetActive(false);
                attack_icon.SetActive(false);
            }
        }

        else
        {
            if (hero1.turn)

            {
                if (hero1.modeIsAttack)
                {
                    move_icon.SetActive(false);
                    attack_icon.SetActive(true);
                }

                else if (!hero1.modeIsAttack)
                {
                    attack_icon.SetActive(false);
                    move_icon.SetActive(true);
                }

            }

            else
            {
                move_icon.SetActive(false);
                attack_icon.SetActive(false);
            }
        }

    }
}
