using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class InventoryTest
{
    [Test]
    public void AddItemToInventory()
    {
        GameObject gameObject = new GameObject();
        Inventory inventory = gameObject.AddComponent<Inventory>();

        GameObject slot = new GameObject();
        GameObject slotChild0 = new GameObject();
        GameObject slotChild1 = new GameObject();

        slotChild0.SetActive(false);
        slotChild1.AddComponent<Image>();
 
        slotChild1.transform.SetParent(slotChild0.transform);
        slotChild0.transform.SetParent(slot.transform);
        slot.transform.SetParent(gameObject.transform);

        inventory.Awake();

        ItemCF item = new ItemCF();
        GameObject gameObjectItem = new GameObject();

        item.nameItem = "Siekiera";
        item.gObject = gameObjectItem;
        item.space = "hand";
        item.attack = 5;

        inventory.AddItem(item);

        Assert.AreEqual(1, inventory.GetItems().Count);
        Assert.AreEqual(item.gObject, inventory.GetItems()[0].gameObject);
        Assert.AreEqual(item.nameItem, inventory.GetItems()[0].name);
        Assert.AreEqual(slotChild0, inventory.GetItems()[0].slot);
        Assert.AreEqual(slotChild1.GetComponent<Image>().sprite, inventory.GetItems()[0].sprite);
        Assert.AreEqual(1, inventory.GetItems()[0].quantity);
        Assert.AreEqual(item.space, inventory.GetItems()[0].space);
        Assert.AreEqual(item.attack, inventory.GetItems()[0].attack);
        Assert.AreEqual(item.hp, inventory.GetItems()[0].hp);
    }

    [Test]
    public void QuantityItemsInInventory()
    {
        GameObject gameObject = new GameObject();
        Inventory inventory = gameObject.AddComponent<Inventory>();

        GameObject slot = new GameObject();
        GameObject slotChild0 = new GameObject();
        GameObject slotChild1 = new GameObject();

        slotChild0.SetActive(false);
        slotChild0.AddComponent<TextMeshProUGUI>();
        slotChild1.AddComponent<Image>();

        slotChild1.transform.SetParent(slotChild0.transform);
        slotChild0.transform.SetParent(slot.transform);
        slot.transform.SetParent(gameObject.transform);

        inventory.Awake();

        ItemCF item = new ItemCF();
        GameObject gameObjectItem = new GameObject();

        item.nameItem = "Siekiera";
        item.gObject = gameObjectItem;
        item.space = "hand";
        item.attack = 5;

        inventory.AddItem(item);
        inventory.AddItem(item);

        Assert.AreEqual(2, inventory.GetItems()[0].quantity);
    }

    [Test]
    public void DiscardItem()
    {
        GameObject gameObject = new GameObject();
        Inventory inventory = gameObject.AddComponent<Inventory>();

        GameObject slot = new GameObject();
        GameObject slotChild0 = new GameObject();
        GameObject slotChild1 = new GameObject();

        slotChild0.SetActive(false);
        slotChild0.AddComponent<TextMeshProUGUI>();

        slotChild1.AddComponent<Image>();

        slotChild1.transform.SetParent(slotChild0.transform);
        slotChild0.transform.SetParent(slot.transform);
        slot.transform.SetParent(gameObject.transform);

        inventory.Awake();
        inventory.player = new GameObject().transform;

        ItemCF item = new ItemCF();
        GameObject gameObjectItem = new GameObject();

        Mesh mesh = new Mesh();

        Vector3[] vertices = {
            new Vector3(0, 0, 0),
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 0),
        };

        int[] triangles = {
            0, 1, 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        gameObjectItem.AddComponent<MeshFilter>().mesh = mesh;


        item.nameItem = "Siekiera";
        item.gObject = gameObjectItem;
        item.space = "hand";
        item.attack = 5;

        inventory.AddItem(item);
        inventory.DiscardTheItem(slotChild1);

        Assert.AreEqual(0, inventory.GetItems().Count);
    }
}
