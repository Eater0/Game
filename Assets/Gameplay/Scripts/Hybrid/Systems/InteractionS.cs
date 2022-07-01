using Unity.Entities;
using UnityEngine;

class InteractionS : ComponentSystem
{
    GameObject bar;

    protected override void OnUpdate()
    {
        Entities.ForEach((AreaOfInteractionC areaOfInteraction, GameObjectC gameObject, CameraC camera, OrAliveC orAlive) =>
        {
            bar = gameObject.value.transform.GetChild(2).
                    GetChild(0).
                    GetChild(0).
                    gameObject;

            if (Vector3.Distance(gameObject.value.transform.position, areaOfInteraction.point.position) <= areaOfInteraction.reaction)
            {
                if (!orAlive.value)
                {
                    bar.SetActive(false);
                }
                else
                {
                    bar.SetActive(true);

                    if (!gameObject.name.Equals("Golem"))                           bar.transform.forward = camera.value.forward;

                    if (gameObject.value.GetComponent<Escape>())                    gameObject.value.GetComponent<Escape>().enabled = true;
                    else if (!gameObject.value.GetComponent<StoppingC>().value)     gameObject.value.GetComponent<Attack>().enabled = true;
                    else                                                            gameObject.value.GetComponent<Attack>().enabled = false;
                }
            }
            else if (Vector3.Distance(gameObject.value.transform.position, areaOfInteraction.point.position) >= areaOfInteraction.forgiveness)
            {
                bar.SetActive(false);
                bar.transform.GetChild(0).
                    GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

                if (gameObject.value.GetComponent<Escape>())    gameObject.value.GetComponent<Escape>().enabled = false;
                else                                            gameObject.value.GetComponent<Attack>().enabled = false;
            }
        });
    }
}
