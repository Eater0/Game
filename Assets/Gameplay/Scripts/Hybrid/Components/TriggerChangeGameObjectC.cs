using UnityEngine;

public class TriggerChangeGameObjectC : MonoBehaviour
{
    public GameObject currentGameObject;
    public GameObject doChangeGameObject;
    public GameObject doChangeGameObject1;
    public bool isTrigger = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.name.Equals("Trunk") && !other.name.Equals("Character"))
        {
            isTrigger = true;
        }
    }
}
