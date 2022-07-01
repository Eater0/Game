using Unity.Entities;
using UnityEngine;

class AnimalAttackAnimatorS : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((Animator animator, AttackC attack) =>
        {
            if (attack.value)
            {
                animator.SetTrigger("Attack");
            }
        });
    }
}
