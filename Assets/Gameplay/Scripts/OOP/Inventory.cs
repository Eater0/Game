using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Transform player;

    GameObject[] slots;
    List<Item> items = new List<Item>();

    GameObject item;
    GameObject created;

    bool orRepeatsItself;

    public void Awake()
    {
        slots = new GameObject[gameObject.transform.childCount];

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            slots[i] = gameObject.transform.GetChild(i).gameObject;
        }
    }

    public void AddItem(ItemCF itemCF)
    {
        orRepeatsItself = false;

        foreach (Item item in items)
        {
            if (item.name.Equals(itemCF.nameItem))
            {
                item.quantity++;
                item.slot.GetComponentInChildren<TextMeshProUGUI>().text = item.quantity.ToString();

                orRepeatsItself = true;
            }
        }

        if (!orRepeatsItself)
        {
            foreach (GameObject slot in slots)
            {
                if (!slot.transform.GetChild(0).gameObject.activeSelf)
                {
                    Item item = new Item();

                    item.gameObject = itemCF.gObject;

                    item.name = itemCF.nameItem;

                    item.slot = slot.transform.GetChild(0).gameObject;
                    item.slot.SetActive(!item.slot.activeSelf);

                    item.slot.transform.GetChild(0).GetComponent<Image>().sprite = itemCF.sprite;
                    item.sprite = itemCF.sprite;

                    item.quantity = 1;

                    item.space = itemCF.space;

                    item.attack = itemCF.attack;
                    item.hp = itemCF.hp;

                    items.Add(item);
                    Debug.Log("tptp");
                    break;
                }
            }
        }
    }

    public void DiscardTheItem(GameObject buttonX)
    {
        item = buttonX.transform.parent.gameObject;

        foreach (Item itemm in items)
        {
            if (itemm.slot == item)
            {
                itemm.quantity--;
                created = Instantiate(itemm.gameObject, player.position + player.forward * 2 + new Vector3(0, 1, 0), player.rotation * Quaternion.Euler(90, 90, 0));
                created.transform.position += created.transform.up * -(created.GetComponent<MeshFilter>().mesh.bounds.size.y * created.transform.localScale.y / 2);

                if (itemm.quantity == 0)
                {
                    item.SetActive(false);
                    items.Remove(itemm);
                }
                else if (itemm.quantity == 1)
                {
                    itemm.slot.GetComponentInChildren<TextMeshProUGUI>().text = null;
                }
                else
                {
                    itemm.slot.GetComponentInChildren<TextMeshProUGUI>().text = itemm.quantity.ToString();
                }

                orRepeatsItself = true;

                break;
            }
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
