using Unity.Entities;
using UnityEngine;

public class ChangeWithoutChildS : ComponentSystem
{
    GameObject gameObject;

    protected override void OnUpdate()
    {
        Entities.ForEach((ChangeWithoutChildC changeWithoutChild) =>
        {
            if (changeWithoutChild.current.transform.childCount == 0)
            {
                MonoBehaviour.Destroy(changeWithoutChild.current);
                gameObject = MonoBehaviour.Instantiate(changeWithoutChild.doChange, changeWithoutChild.current.transform.position, changeWithoutChild.current.transform.rotation);

                gameObject.GetComponent<ID>().membership = changeWithoutChild.GetComponent<ID>().membership;
                gameObject.GetComponent<ID>().id = changeWithoutChild.GetComponent<ID>().id;
            }
        });
    }
}
