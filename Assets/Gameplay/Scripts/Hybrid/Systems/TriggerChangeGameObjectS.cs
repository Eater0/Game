using Unity.Entities;
using UnityEngine;

class TriggerChangeGameObjectS : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((TriggerChangeGameObjectC trigger) =>
        {
            if (trigger.isTrigger)
            {
                MonoBehaviour.Destroy(trigger.currentGameObject);
                MonoBehaviour.Instantiate(trigger.doChangeGameObject, trigger.currentGameObject.transform.position, trigger.currentGameObject.transform.rotation);
                MonoBehaviour.Instantiate(trigger.doChangeGameObject1, trigger.currentGameObject.transform.position + trigger.currentGameObject.transform.up * 3.9f, trigger.currentGameObject.transform.rotation * Quaternion.Euler(90, -90, 0));
                MonoBehaviour.Instantiate(trigger.doChangeGameObject1, trigger.currentGameObject.transform.position + trigger.currentGameObject.transform.up * 3.3f, trigger.currentGameObject.transform.rotation * Quaternion.Euler(90, 90, 0));
            }
        });
    }
}
