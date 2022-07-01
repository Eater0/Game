using UnityEngine;

public class CharacterChangeGameObjectC : MonoBehaviour
{
    public GameObject current;
    public GameObject doChange;
    public GameObject hand;
    public string reactTo;
    public bool isTrigger;

    void Awake()
    {
        hand = GameObject.Find("Wrist.R_end");
    }

    void OnTriggerStay(Collider other)
    {
        if (hand.transform.childCount == 1 &&
            hand.GetComponentInChildren<NameItem>().nameItem.Equals(reactTo))
        {
            isTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        isTrigger = false;
    }
}
