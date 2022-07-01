using Unity.Entities;
using UnityEngine;

public class PlayerReceivedInjuriesS : ComponentSystem
{
    RectTransform healthBar;

    protected override void OnUpdate()
    {
        /*Entities.ForEach((StaticsCF statics, TriggerDamageC triggerDamage, GameObjectC gameObjectC) =>
        {
            healthBar = gameObjectC.transform.GetChild(4).
                GetChild(0).
                GetChild(0).
                GetChild(0).
                GetComponent<RectTransform>();

            if (triggerDamage && triggerDamage.attack != 0)
            {
                healthBar.anchorMax = new Vector2(healthBar.anchorMax.x - (float)triggerDamage.attack / statics.hp, 1);
            }
        });*/
    }
}
