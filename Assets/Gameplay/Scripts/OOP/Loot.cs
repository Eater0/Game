using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Loot : MonoBehaviour
{
    GameObject[] slots;

    void Awake()
    {
        slots = new GameObject[gameObject.transform.GetChild(0).childCount];

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = gameObject.transform.GetChild(0).
                GetChild(i).gameObject;
        }
    }

    public void AddItems(ItemCF[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            slots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].sprite;
            slots[i].SetActive(true);
        }

        gameObject.SetActive(true);

        StartCoroutine(Stop(items));
    }

    IEnumerator Stop(ItemCF[] items)
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);

        foreach (GameObject slot in slots)
        {
            slot.SetActive(false);
        }
    }
}
