using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Eats : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    RectTransform healthBar;

    List<Item> items;
    float sumHp;

    public void Food(GameObject button)
    {
        items = inventory.GetItems();

        foreach (Item item in items)
        {
            if (item.slot == button && item.space.Equals("food"))
            {
                sumHp = healthBar.anchorMax.x + (float)item.hp / 100;

                if (sumHp <= 1)     healthBar.anchorMax = new Vector2(sumHp, 1);
                else                healthBar.anchorMax = new Vector2(1, 1);

                item.quantity--;

                if (item.quantity == 0)
                {
                    button.SetActive(false);
                    items.Remove(item);
                }
                else if (item.quantity == 1)
                {
                    item.slot.
                        transform.GetChild(1).
                        GetComponent<TextMeshProUGUI>().text = "";
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
