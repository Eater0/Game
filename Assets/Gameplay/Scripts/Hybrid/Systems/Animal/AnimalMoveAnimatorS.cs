using Unity.Entities;
using UnityEngine;

class AnimalMoveAnimatorS : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.
            WithAll<AnimalT>().
            ForEach((Animator animator, CharacterController controller) =>
        {
            animator.SetFloat("Move", controller.velocity.magnitude / 10, 0.2f, Time.DeltaTime);
        });
    }
}
