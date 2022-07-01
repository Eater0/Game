using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuttingOnArmament : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    StaticsCF primaryStaticsCharacter;
    [SerializeField]
    GameObject hand;
    [SerializeField]
    GameObject head;
    [SerializeField]
    GameObject chest;
    [SerializeField]
    GameObject armL;
    [SerializeField]
    GameObject armR;
    [SerializeField]
    GameObject forearmL;
    [SerializeField]
    GameObject forearmR;

    GameObject placeOnTheBody;
    int index;
    bool side1;
    bool side2;
    ItemCF[] currentItems;
    StaticsCF statics;
    List<Item> items;

    void Awake()
    {
        if (Manager.GetData() == null)
        {
            currentItems = new ItemCF[7];

            for (int i = 0; i < currentItems.Length; i++)
            {
                currentItems[i] = new ItemCF();
            }
        }
    }

    public void PuttingOn(GameObject button)
    {
        items = inventory.GetItems();
        placeOnTheBody = null;

        foreach (Item item in items)
        {
            if (item.slot == button)
            {
                Place(item);

                if (placeOnTheBody != null)
                {
                    if ((index == 0 || index == 1) && placeOnTheBody.transform.childCount == 1)
                    {
                        QuantityManagement();
                    }
                    else if (placeOnTheBody.transform.childCount == 2)
                    {
                        QuantityManagement();
                    }

                    item.quantity--;

                    SettingUp(item);

                    if (item.quantity == 0)
                    {
                        button.SetActive(false);
                        items.Remove(item);
                    }
                    else if (item.quantity == 1)
                    {
                        item.slot.
                            transform.GetChild(1).
                            GetComponent<TextMeshProUGUI>().text = null;
                    }
                    else
                    {
                        item.slot.
                            transform.GetChild(1).
                            GetComponent<TextMeshProUGUI>().text = item.quantity.ToString();
                    }

                    break;
                }
            }
        }
    }

    void Place(Item item)
    {
        switch (item.space)
        {
            case "hand":
                placeOnTheBody = hand;
                index = 0;
                break;
            case "head":
                placeOnTheBody = head;
                index = 1;
                break;
            case "chest":
                placeOnTheBody = chest;
                index = 2;
                break;
            case "arm":
                placeOnTheBody = armL;
                index = 3;
                break;
            case "forearm":
                placeOnTheBody = forearmL;
                index = 4;
                break;
        }
    }

    void SettingUp(Item item)
    {
        Instantiate(item.gameObject, placeOnTheBody.transform.position, placeOnTheBody.transform.rotation * Quaternion.Euler(item.gameObject.GetComponent<PositionAndRotationCF>().rotation)).transform.parent = placeOnTheBody.transform;

        if (index < 2)
        {
            Destroy(placeOnTheBody.GetComponentInChildren<Rigidbody>());
            Destroy(placeOnTheBody.GetComponentInChildren<BoxCollider>());
            Destroy(placeOnTheBody.GetComponentInChildren<ItemCF>());

            foreach (Transform child in placeOnTheBody.transform)
            {
                child.GetComponent<Transform>().localPosition = item.gameObject.GetComponent<PositionAndRotationCF>().position;
                child.gameObject.AddComponent<NameItem>().nameItem = item.name;
            }
        }
        else if (index > 4)
        {
            Destroy(placeOnTheBody.transform.GetChild(1).GetComponent<Rigidbody>());
            Destroy(placeOnTheBody.transform.GetChild(1).GetComponent<BoxCollider>());
            Destroy(placeOnTheBody.transform.GetChild(1).GetComponent<ItemCF>());

            placeOnTheBody.transform.GetChild(1).GetComponent<Transform>().localPosition = new Vector3(-item.gameObject.GetComponent<PositionAndRotationCF>().position.x, item.gameObject.GetComponent<PositionAndRotationCF>().position.y, item.gameObject.GetComponent<PositionAndRotationCF>().position.z);
            placeOnTheBody.transform.GetChild(1).gameObject.AddComponent<NameItem>().nameItem = item.name;
        }
        else
        {
            Destroy(placeOnTheBody.transform.GetChild(1).GetComponent<Rigidbody>());
            Destroy(placeOnTheBody.transform.GetChild(1).GetComponent<BoxCollider>());
            Destroy(placeOnTheBody.transform.GetChild(1).GetComponent<ItemCF>());

            placeOnTheBody.transform.GetChild(1).GetComponent<Transform>().localPosition = item.gameObject.GetComponent<PositionAndRotationCF>().position;
            placeOnTheBody.transform.GetChild(1).gameObject.AddComponent<NameItem>().nameItem = item.name;
        }

        currentItems[index].nameItem = item.name;
        currentItems[index].gObject = item.gameObject;
        currentItems[index].sprite = item.sprite;
        currentItems[index].space = item.space;
        currentItems[index].attack = item.attack;
        currentItems[index].hp = item.hp;

        statics = primaryStaticsCharacter;
        statics.attack += item.attack;
        statics.hp += item.hp;
    }

    void QuantityManagement()
    {
        if (index < 2)
        {
            Destroy(placeOnTheBody.transform.GetChild(0).gameObject);
            inventory.GetComponent<Inventory>().AddItem(currentItems[index]);
            statics.attack -= currentItems[index].attack;
            statics.hp -= currentItems[index].hp;
        }
        else if (index == 3)    Side(side1);
        else if (index == 4)    Side(side2);
        else
        {
            Object.DestroyImmediate(placeOnTheBody.transform.GetChild(1).gameObject);
            inventory.GetComponent<Inventory>().AddItem(currentItems[index]);
            statics.attack -= currentItems[index].attack;
            statics.hp -= currentItems[index].hp;
        }
    }

    void Side(bool side)
    {
        if (!side)
        {
            if (index == 3)
            {
                placeOnTheBody = armR;
                side1 = true;
            }
            else
            {
                placeOnTheBody = forearmR;
                side2 = true;
            }

            index += 2;

            if (placeOnTheBody.transform.childCount == 2)
            {
                Object.DestroyImmediate(placeOnTheBody.transform.GetChild(1).gameObject);

                inventory.GetComponent<Inventory>().AddItem(currentItems[index]);
                statics.attack -= currentItems[index].attack;
                statics.hp -= currentItems[index].hp;
            }
        }
        else
        {
            Object.DestroyImmediate(placeOnTheBody.transform.GetChild(1).gameObject);

            inventory.GetComponent<Inventory>().AddItem(currentItems[index]);
            statics.attack -= currentItems[index].attack;
            statics.hp -= currentItems[index].hp;

            if (index == 3)     side1 = false;
            else                side2 = false;
        }
    }

    public void SetData(ItemCF[] currentItems, StaticsCF statics)
    {
        this.currentItems = currentItems;
        this.statics = statics;
    }

    public ItemCF[] GetCurrentItems()
    {
        return currentItems;
    }
}
