using Unity.Entities;
using UnityEngine;

class PlayerAnimatorS :  ComponentSystem
{
    Transform child;

    protected override void OnUpdate()
    {
        Entities.ForEach((Animator animator, NCIsGroundedC isGrounded, InputJumpC inputJ, CharacterController controller, HandC hand) =>
        {
            if (hand.value.transform.childCount == 1)
            {
                foreach (Transform childd in hand.value.transform)
                {
                    child = childd;
                }
            }

            if (isGrounded.value)
            {
                animator.SetBool(inputJ.jump, false);

                if (Input.GetMouseButtonDown(0) && Time.DeltaTime != 0)
                {
                    if (!child)
                    {
                        animator.SetTrigger("Hit");
                    }
                    else if (child)
                    {
                        animator.SetTrigger("Slashing");
                    }
                }
                else
                {
                    animator.ResetTrigger("Hit");
                    animator.ResetTrigger("Slashing");

                    animator.SetFloat("Move", controller.velocity.magnitude / 10, 0.2f, Time.DeltaTime);
                }
            }
            else
            {
                animator.SetBool(inputJ.jump, true);
            }
        });
    }
}
