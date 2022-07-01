using UnityEngine;

class RenewableData : MonoBehaviour
{
    Transform player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (gameObject.GetComponent<Attack>())
        {
            gameObject.GetComponent<Attack>().victim = player;
        }
        else
        {
            gameObject.GetComponent<Escape>().player = player;
        }

        gameObject.GetComponent<CameraC>().value = Camera.main.transform;
        gameObject.GetComponent<AreaOfInteractionC>().point = player;
        gameObject.GetComponent<RagdollC>().ignore = player.GetComponent<BoxCollider>();
    }
}
