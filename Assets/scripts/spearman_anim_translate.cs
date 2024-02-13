using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spearman_anim_translate : MonoBehaviour
{
    private Animator animator;
    public GameObject hero;
    private HeroGR_Mov_Att hero_stares;
    void Start()
    {
        hero_stares = hero.GetComponent<HeroGR_Mov_Att>();
        animator = this.GetComponent<Animator>();
    }

    
    void Update()
    {
        if (hero_stares.isMoving)
        {
            animator.SetBool("IsWalking", true);
        }

        if (!hero_stares.isMoving)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGoingToJump", false);
        }

        if (hero_stares.jumpingUp)
        {
            animator.SetBool("IsGoingToJump", true);
        }

        if (hero_stares.fallingDown)
        {
            animator.SetBool("IsFalling", true);
        }

        if (!hero_stares.fallingDown)
        {
            animator.SetBool("IsFalling", false);
        }

        if (hero_stares.is_attacking)
        {
            animator.SetBool("IsAttacking", true);
        }

        if (!hero_stares.is_attacking)
        {
            animator.SetBool("IsAttacking", false);
        }
    }
}
