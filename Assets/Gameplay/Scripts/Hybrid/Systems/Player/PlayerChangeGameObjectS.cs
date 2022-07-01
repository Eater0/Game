using Unity.Entities;
using UnityEngine;

class PlayerChangeGameObjectS : ComponentSystem
{
    GameObject gameObject;

    protected override void OnUpdate()
    {
        Entities.ForEach((CharacterChangeGameObjectC changeGameObject) =>
        {
            if (changeGameObject.isTrigger && Input.GetMouseButtonDown(0))
            {
                MonoBehaviour.Destroy(changeGameObject.current);
                gameObject = MonoBehaviour.Instantiate(changeGameObject.doChange, changeGameObject.current.transform.position, changeGameObject.current.transform.rotation);

                gameObject.transform.localScale = changeGameObject.current.transform.localScale;

                gameObject.GetComponent<ID>().membership = changeGameObject.GetComponent<ID>().membership;
                gameObject.GetComponent<ID>().id = changeGameObject.GetComponent<ID>().id;
            }
        });
    }
}
