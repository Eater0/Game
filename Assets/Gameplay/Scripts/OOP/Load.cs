using UnityEngine;

public class Load : MonoBehaviour
{
    ID[] ids;
    GameObject destroy;
    GameObject doChange;
    GameObject clone;
    Vector3 position;
    Quaternion rotation;

    GameObject placeOnTheBody;
    int index;
    bool side1;
    bool side2;
    ItemCF[] currentItems = new ItemCF[7];
    StaticsCF statics;

    public void LoadSave(Inventory inventory, ItemCF[] items, GameObject[] buildings, PuttingOnArmament puttingOnArmament, GameObject character, GameObject[] armament)
    {
        Data data = Manager.GetData();
        ids = FindObjectsOfType<ID>();

        foreach (RevivalM revival in data.revivals)
        {
            foreach (ID id in ids)
            {
                if (revival.id == id.id && revival.membership.Equals(id.membership) && !id.GetComponent<AnimalT>())
                {
                    destroy = id.gameObject;
                    Destroy(destroy);

                    doChange = destroy.GetComponent<CharacterChangeGameObjectC>().doChange;

                    clone = Instantiate(doChange, destroy.transform.position, destroy.transform.rotation);

                    Destroy(clone.transform.GetChild(0).gameObject);

                    clone.GetComponent<ID>().membership = id.membership;
                    clone.GetComponent<ID>().id = id.id;
                    clone.GetComponent<RevivalC>().meter = revival.meter;

                    break;
                }
            }
        }

        foreach (RevivalCreatureM revival in data.revivalCreatures)
        {
            foreach (ID id in ids)
            {
                if (revival.revival.id == id.id && revival.revival.membership.Equals(id.membership))
                {
                    destroy = id.gameObject;
                    Destroy(destroy);

                    doChange = destroy.GetComponent<RespC>().doChange;

                    position.x = revival.position.x;
                    position.y = revival.position.y;
                    position.z = revival.position.z;

                    rotation.x = revival.rotation.x;
                    rotation.y = revival.rotation.y;
                    rotation.z = revival.rotation.z;
                    rotation.w = revival.rotation.w;

                    clone = Instantiate(doChange, position, rotation);

                    clone.GetComponent<ID>().membership = id.membership;
                    clone.GetComponent<ID>().id = id.id;
                    clone.GetComponent<RevivalC>().meter = revival.revival.meter;
                }
            }
        }

        foreach (CreatureM creature in data.creatures)
        {
            foreach (ID id in ids)
            {
                if (creature.id == id.id && creature.membership.Equals(id.membership))
                {
                    id.gameObject.transform.position = new Vector3(creature.position.x, creature.position.y, creature.position.z);
                    id.gameObject.transform.rotation = new Quaternion(creature.rotation.x, creature.rotation.y, creature.rotation.z, creature.rotation.w);
                    id.transform.GetChild(2).
                        GetChild(0).
                        GetChild(0).
                        GetChild(0).
                        GetComponent<RectTransform>().
                        anchorMax = new Vector2(creature.percentHp, 1);
                }
            }
        }

        foreach (var itemM in data.itemsM)
        {
            foreach (var item in items)
            {
                if (itemM.name.Equals(item.nameItem))
                {
                    for (int i = 0; i < itemM.quantity; i++)
                    {
                        inventory.AddItem(item);
                    }

                    break;
                }
            }
        }

        foreach (var buildM in data.buildings)
        {
            foreach (var build in buildings)
            {
                position.x = buildM.position.x;
                position.y = buildM.position.y;
                position.z = buildM.position.z;

                rotation.x = buildM.rotation.x;
                rotation.y = buildM.rotation.y;
                rotation.z = buildM.rotation.z;
                rotation.w = buildM.rotation.w;

                if (buildM.type.Equals("floor") && build.GetComponent<FloorT>())            Instantiate(build, position, rotation);
                else if (buildM.type.Equals("paries") && build.GetComponent<PariesT>())     Instantiate(build, position, rotation);
                else if (buildM.type.Equals("roof") && build.GetComponent<RoofT>())         Instantiate(build, position, rotation);
                else if (buildM.type.Equals("stairs") && build.GetComponent<StairsT>())     Instantiate(build, position, rotation);
                else if (buildM.type.Equals("wall") && build.GetComponent<WallT>())         Instantiate(build, position, rotation);
                else                                                                        Instantiate(build, position, rotation);
            }
        }

        character.transform.GetChild(2).
            GetChild(0).
            GetChild(0).
            GetChild(0).
            GetComponent<RectTransform>().
            anchorMax = new Vector2(data.character.percentHp, 1);

        character.transform.GetChild(2).
            GetChild(1).
            GetChild(0).
            GetChild(0).
            GetComponent<RectTransform>().
            anchorMax = new Vector2(data.character.percentThirst, 1);

        for (int i = 0; i < currentItems.Length; i++)
        {
            currentItems[i] = new ItemCF();
        }

        foreach (var itemm in data.armament)
        {
            foreach (var item in items)
            {
                if (itemm.Equals(item.nameItem))
                {
                    switch (item.space)
                    {
                        case "hand":
                            placeOnTheBody = armament[0];
                            index = 0;
                            break;
                        case "head":
                            placeOnTheBody = armament[1];
                            index = 1;
                            break;
                        case "chest":
                            placeOnTheBody = armament[2];
                            index = 2;
                            break;
                        case "arm":
                            placeOnTheBody = armament[3];
                            index = 3;
                            break;
                        case "forearm":
                            placeOnTheBody = armament[4];
                            index = 4;
                            break;
                    }

                    if (index == 3 && side1)
                    {
                        index += 2;
                        placeOnTheBody = armament[index];
                    }
                    else if (index == 4 && side2)
                    {
                        index += 2;
                        placeOnTheBody = armament[index];
                    }

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

                    currentItems[index] = item;

                    statics = character.GetComponent<StaticsCF>();
                    statics.attack += item.attack;
                    statics.hp += item.hp;

                    if (index == 3)
                    {
                        side1 = true;
                    }
                    else if (index == 4)
                    {
                        side2 = true;
                    }

                    break;
                }
            }
        }

        puttingOnArmament.SetData(currentItems, statics);
    }
}
