using Unity.Entities;
using UnityEngine;

class StoppingS : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Animator animator, StoppingC stopping) =>
        {
            if (animator.GetAnimatorTransitionInfo(0).duration == 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Move Tree"))
            {
                stopping.value = false;
            }
            else
            {
                stopping.value = true;
            }
        });
    }
}
