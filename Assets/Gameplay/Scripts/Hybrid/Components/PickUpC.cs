using UnityEngine;

public class PickUpC : MonoBehaviour
{
    public Inventory inventory;
    public Loot loot;
    public KeyCode keyCode;
    public Collider coll;

    void OnTriggerExit(Collider other)
    {
        coll = null;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.tag.Equals("Terrain") && !other.gameObject.tag.Equals("Ignore"))
        {
            coll = other;
        }
    }
}
