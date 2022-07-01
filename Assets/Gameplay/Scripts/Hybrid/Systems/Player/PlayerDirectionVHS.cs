using Unity.Entities;
using UnityEngine;

class PlayerDirectionHVS : ComponentSystem
{
    float x;
    float z;

    protected override void OnUpdate()
    {
        Entities.ForEach((InputHorizontalAndVerticalC input, DirectionC direction, NCIsGroundedC isGround, StoppingC stopping) =>
        {
            x = Input.GetAxis(input.HorizonstalAxis);
            z = Input.GetAxis(input.VerticalAxis);

            if (stopping.value || !isGround.value)
            {
                direction.value = Vector3.zero;
            }
            else
            {
                direction.value = new Vector3(x, 0, z).normalized;
            }
        });
    }
}
