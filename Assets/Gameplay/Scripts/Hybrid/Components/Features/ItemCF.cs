using UnityEngine;

public class ItemCF : MonoBehaviour
{
    public string nameItem;
    public Sprite sprite;
    public GameObject gObject;

    [Header("Avaiable values: hand, head, chest, arm, forearm, environment, food and nothing")]
    public string space;
    public int attack;
    public int hp;
}
