using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPS_anim_translate : MonoBehaviour
{
    private Animator animator;
    public GameObject npc;
    private NPCMove npc_states;
    void Start()
    {
        npc_states = npc.GetComponent<NPCMove>();
        animator = this.GetComponent<Animator>();
    }


    void Update()
    {
        if (npc_states.isMoving)
        {
            animator.SetBool("IsWalking", true);
        }

        if (!npc_states.isMoving)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsFalling", false);
            animator.SetBool("IsGoingToJump", false);
        }

        if (npc_states.jumpingUp)
        {
            animator.SetBool("IsGoingToJump", true);
        }

        if (npc_states.fallingDown)
        {
            animator.SetBool("IsFalling", true);
        }

        if (!npc_states.fallingDown)
        {
            animator.SetBool("IsFalling", false);
        }

        if (npc_states.is_attacking)
        {
            animator.SetBool("IsAttacking", true);
        }

        if (!npc_states.is_attacking)
        {
            animator.SetBool("IsAttacking", false);
        }
    }
}
