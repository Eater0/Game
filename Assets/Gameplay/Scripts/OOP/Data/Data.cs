using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public string dateDay;
    public string dateDate;
    public List<RevivalM> revivals;
    public List<RevivalCreatureM> revivalCreatures;
    public List<CreatureM> creatures;
    public List<BuildingM> buildings;
    public CharacterM character;
    public List<ItemM> itemsM;
    public List<string> armament;

    public Data(List<GameObject> revivalEnviroment, List<GameObject> revivalCreatures, GameObject[] creatures, GameObject[] buildings, GameObject character, List<Item> itemsInInventory, ItemCF[] armament)
    {
        DateTime time = DateTime.Now;
        dateDay = time.ToString("dd MMMM");
        dateDate = time.ToString("HH:mm\tyyyy");

        revivals = new List<RevivalM>();
        this.revivalCreatures = new List<RevivalCreatureM>();
        this.creatures = new List<CreatureM>();
        this.buildings = new List<BuildingM>();
        itemsM = new List<ItemM>();
        this.armament = new List<string>();

        foreach (var gameObject in revivalEnviroment)
        {
            RevivalM revival = new RevivalM();
            revival.id = gameObject.GetComponent<ID>().id;
            revival.membership = gameObject.GetComponent<ID>().membership;
            revival.meter = gameObject.GetComponent<RevivalC>().meter;

            revivals.Add(revival);
        }

        foreach (var gameObject in revivalCreatures)
        {
            RevivalCreatureM revival = new RevivalCreatureM();

            revival.revival = new RevivalM();
            revival.revival.id = gameObject.GetComponent<ID>().id;
            revival.revival.membership = gameObject.GetComponent<ID>().membership;
            revival.revival.meter = gameObject.GetComponent<RevivalC>().meter;

            revival.position = new PositionM();
            revival.rotation = new RotationM();

            TransformPR(revival.position, revival.rotation, gameObject);

            this.revivalCreatures.Add(revival);
        }

        foreach (var gameObject in creatures)
        {
            if (gameObject.GetComponent<ID>() != null)
            {
                CreatureM creature = new CreatureM();
                creature.id = gameObject.GetComponent<ID>().id;
                creature.membership = gameObject.GetComponent<ID>().membership;

                creature.position = new PositionM();
                creature.rotation = new RotationM();

                creature.percentHp = gameObject.transform.GetChild(2).
                    GetChild(0).
                    GetChild(0).
                    GetChild(0).
                    GetComponent<RectTransform>().
                    anchorMax.x;

                TransformPR(creature.position, creature.rotation, gameObject);
                this.creatures.Add(creature);
            }
        }

        foreach (var gameObject in buildings)
        {
            BuildingM building = new BuildingM();

            if (gameObject.GetComponent<FloorT>())          building.type = "floor";
            else if (gameObject.GetComponent<PariesT>())    building.type = "paries";
            else if (gameObject.GetComponent<RoofT>())      building.type = "roof";
            else if (gameObject.GetComponent<StairsT>())    building.type = "stairs";
            else if (gameObject.GetComponent<WallT>())      building.type = "wall";
            else                                            building.type = "campfire";

            building.position = new PositionM();
            building.rotation = new RotationM();

            TransformPR(building.position, building.rotation, gameObject);
            this.buildings.Add(building);
        }

        this.character = new CharacterM();
        this.character.percentHp = character.transform.GetChild(2).
                GetChild(0).
                GetChild(0).
                GetChild(0).
                GetComponent<RectTransform>().
                anchorMax.x;

        this.character.percentThirst = character.transform.GetChild(2).
                GetChild(1).
                GetChild(0).
                GetChild(0).
                GetComponent<RectTransform>().
                anchorMax.x;

        this.character.position = new PositionM();
        this.character.rotation = new RotationM();

        TransformPR(this.character.position, this.character.rotation, character);

        foreach (var item in itemsInInventory)
        {
            ItemM itemM = new ItemM();
            itemM.name = item.name;
            itemM.quantity = item.quantity;

            itemsM.Add(itemM);
        }

        if (armament != null)
        {
            foreach (var item in armament)
            {
                if (item.nameItem != null)
                {
                    this.armament.Add(item.nameItem);
                }
            }
        }
    }

    void TransformPR(PositionM position, RotationM rotation, GameObject gameObject)
    {
        position.x = gameObject.transform.position.x;
        position.y = gameObject.transform.position.y;
        position.z = gameObject.transform.position.z;

        rotation.x = gameObject.transform.rotation.x;
        rotation.y = gameObject.transform.rotation.y;
        rotation.z = gameObject.transform.rotation.z;
        rotation.w = gameObject.transform.rotation.w;
    }
}
