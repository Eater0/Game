using System.Collections.Generic;
using TMPro;
using UnityEngine;

class CreateItem : MonoBehaviour
{
    [SerializeField]
    GameObject inventory;
    [SerializeField]
    GameObject rucksack;
    [SerializeField]
    GameObject crafting;
    [SerializeField]
    GameObject current;

    int quantityOfMaterials;
    int j;
    List<Item> items;
    List<Material> needsMaterials = new List<Material>();
    List<int> indexs = new List<int>();
    GameObject create;

    void Awake()
    {
        foreach (Transform child in current.transform.GetChild(1))
        {
            needsMaterials.Add(child.GetComponent<Material>());
        }

        quantityOfMaterials = needsMaterials.Count;
    }

    public void CreateItemm()
    {
        items = inventory.GetComponent<Inventory>().GetItems();
        indexs.Clear();

        CheckTheMaterials();

        if (quantityOfMaterials == indexs.Count)
        {
            RemoveMaterials();

            if (current.GetComponent<ItemCF>().space.Equals("environement"))
            {
                create = Instantiate(current.GetComponent<ItemCF>().gObject);
                create.AddComponent<Construction>();

                crafting.SetActive(false);
                rucksack.SetActive(false);
            }
            else
            {
                inventory.GetComponent<Inventory>().
                    AddItem(current.GetComponent<ItemCF>());
            }
        }
    }

    void CheckTheMaterials()
    {
        foreach (Material needMaterial in needsMaterials)
        {
            for (int  i = 0; i < items.Count; i++)
            {
                if (needMaterial.nameItem.Equals(items[i].name)
                    && needMaterial.quantity <= items[i].quantity)
                {
                    indexs.Add(i);
                    break;
                }
            }
        }
    }

    void RemoveMaterials()
    {
        j = 0;

        foreach (int i in indexs)
        {
            items[i].quantity -= needsMaterials[j].quantity;

            if (items[i].quantity == 0)
            {
                items[i].slot.SetActive(false);
            }
            else if (items[i].quantity == 1)
            {
                items[i].slot.
                    transform.GetChild(1).
                    GetComponent<TextMeshProUGUI>().text = null;
            }
            else
            {
                items[i].slot.
                    transform.GetChild(1).
                    GetComponent<TextMeshProUGUI>().text = items[i].quantity.ToString();
            }

            j++;
        }

        indexs.Sort();

        for (int i = indexs.Count - 1; i >= 0; i--)
        {
            if (items[indexs[i]].quantity == 0)
            {
                items.Remove(items[indexs[i]]);
            }
        }
    }
}
