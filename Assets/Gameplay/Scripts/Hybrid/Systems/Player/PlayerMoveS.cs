using Unity.Entities;
using UnityEngine;

class PlayerMoveS : ComponentSystem
{
    Vector3 velocityV;
    Vector3 velocityY;
    Vector3 currentVelocityJ;
    Vector3 currentVelocityG;

    bool isJump;

    protected override void OnUpdate()
    {
        Entities.ForEach((CharacterController controller, VelocityC velocity, InputJumpC inputJ, GravityC gravity, NCIsGroundedC isGrounded, StoppingC stopping) =>
        {
            velocityV = velocity.value;

            if (isGrounded.value && velocityY.y < 0)
            {
                velocityY.y = -2;
                currentVelocityG = velocityV;

                isJump = false;
            }

            if (isGrounded.value && Input.GetButton(inputJ.jump) && !stopping.value)
            {
                velocityY.y = Mathf.Sqrt(inputJ.height * -2 * gravity.value);
                currentVelocityJ = velocityV;

                isJump = true;
            }

            velocityY.y += gravity.value * Time.DeltaTime;

            controller.Move(velocityY * Time.DeltaTime);

            if (!isGrounded.value)
            {
                if (isJump)     controller.Move(currentVelocityJ * Time.DeltaTime);
                else            controller.Move(currentVelocityG * Time.DeltaTime);
            }
            else if (velocityV != Vector3.zero && !stopping.value)
            {
                controller.Move(velocityV * Time.DeltaTime);
            }
        });
    }
}
