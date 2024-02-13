using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_translate_OW : MonoBehaviour
{
    private Animator animator;
    public GameObject hero_reference;
    private Hero hero;
    void Start()
    {
        hero = hero_reference.GetComponent<Hero>();
        animator = this.GetComponent<Animator>();
    }


    void Update()
    {
        if (hero.forward_is_down)
        {
            animator.SetBool("IsWalking", true);
        }

        if (!hero.forward_is_down)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGoingToJump", false);
        }

        if (hero.jumping_key_is_pressed)
        {
            animator.SetBool("IsGoingToJump", true);
        }

        if (hero.is_in_air)
        {
            animator.SetBool("IsFalling", true);
        }

        if (!hero.is_in_air)
        {
            animator.SetBool("IsFalling", false);
        }

    }
}
