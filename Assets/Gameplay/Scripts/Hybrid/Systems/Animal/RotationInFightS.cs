using Unity.Entities;
using UnityEngine;

class RotationInFightS : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((AttackC attack, Animator animator, GameObjectC gameObject, AreaOfInteractionC areaOfInteraction) =>
        {
            if (!attack.value && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && animator.GetAnimatorTransitionInfo(0).duration == 0)
            {
                gameObject.transform.localRotation = Quaternion.LookRotation(areaOfInteraction.point.position - gameObject.transform.position, Vector3.up);
            }
        });
    }
}
