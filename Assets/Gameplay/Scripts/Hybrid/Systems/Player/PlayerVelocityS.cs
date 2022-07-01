using Unity.Entities;
using UnityEngine;

class PlayerVelocityS : ComponentSystem
{
    Vector3 directionV;
    Vector3 moveDir;
    float targetAngle;

    protected override void OnUpdate()
    {
        Entities.ForEach((CameraC camera, DirectionC direction, SpeedC speed, InputAccelerationC inputA, VelocityC velocity) =>
        {
            directionV = direction.value;

            if (directionV.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(directionV.x, directionV.z) * Mathf.Rad2Deg + camera.value.eulerAngles.y;

                moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;

                if (Input.GetKey(inputA.key))   velocity.value = moveDir.normalized * inputA.speed;
                else                            velocity.value = moveDir.normalized * speed.value;
            }
            else
            {
                velocity.value = Vector3.zero;
            }
        });
    }
}
