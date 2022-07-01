using Unity.Entities;
using UnityEngine;

class RevivalS : ComponentSystem
{
    GameObject gameObject;

    protected override void OnUpdate()
    {
        Entities.ForEach((RevivalC revival) =>
        {
            revival.meter += Time.DeltaTime;

            if (revival.meter > revival.time)
            {
                MonoBehaviour.Destroy(revival.current);
                gameObject = MonoBehaviour.Instantiate(revival.doChange, revival.current.transform.position, revival.current.transform.rotation);

                if (revival.current.name.Equals("Tree00"))
                {
                    gameObject.transform.localScale = revival.current.transform.localScale;
                }

                gameObject.GetComponent<ID>().membership = revival.GetComponent<ID>().membership;
                gameObject.GetComponent<ID>().id = revival.GetComponent<ID>().id;
            }
        });
    }
}
