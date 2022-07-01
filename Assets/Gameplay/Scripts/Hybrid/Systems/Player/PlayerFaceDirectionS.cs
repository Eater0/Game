using Unity.Entities;
using UnityEngine;

class PlayerFaceDirectionS : ComponentSystem
{
    Vector3 directionV;
    float turnSmoothVelocity;
    float targetAngle;
    float angle;

    protected override void OnUpdate()
    {
        Entities.ForEach((Transform transform, DirectionC direction, CameraC camera) =>
        {
            directionV = direction.value;

            if (directionV.magnitude >= 0.1f)
            {
                targetAngle = Mathf.Atan2(directionV.x, directionV.z) * Mathf.Rad2Deg + camera.value.eulerAngles.y;
                angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.035f);

                transform.rotation = Quaternion.Euler(0, angle, 0);
            }
        });
    }
}
