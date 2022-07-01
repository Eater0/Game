using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

class MoveD : SystemBase
{
    protected override void OnUpdate()
    {
        float time = Time.DeltaTime;

        Entities.ForEach((ref Translation translation, in SpeedD speed) =>
        {
            translation.Value.x += speed.value * time;

            if (translation.Value.x > 2000)
            {
                translation.Value.x = -2000;
            }
        }).ScheduleParallel();
    }
}
