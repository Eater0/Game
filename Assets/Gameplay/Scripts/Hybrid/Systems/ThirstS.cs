using Unity.Entities;
using UnityEngine;

class ThirstS : ComponentSystem
{
    RectTransform thirstBar;

    protected override void OnUpdate()
    {
        Entities.ForEach((GameObjectC gameObject, ThirstC thirst, PickUpC pickUp) =>
        {
            thirstBar = gameObject.transform.GetChild(2).
                GetChild(1).
                GetChild(0).
                GetChild(0)
                .GetComponent<RectTransform>();

            thirstBar.anchorMax = new Vector2(thirstBar.anchorMax.x - thirst.lossPercentThrist * Time.DeltaTime, 1);

            if (Input.GetKeyDown(pickUp.keyCode) && pickUp.coll &&
                pickUp.coll.tag.Equals("Water"))
            {
                thirstBar.anchorMax = new Vector2(1, 1);
            }
        });
    }
}
