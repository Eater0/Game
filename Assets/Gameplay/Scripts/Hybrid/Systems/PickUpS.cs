using Unity.Entities;
using UnityEngine;

class PickUpS : ComponentSystem
{
    Collider collider;

    protected override void OnUpdate()
    {
        Entities.ForEach((PickUpC pickUp) =>
        {
            collider = pickUp.coll;

            if (Input.GetKeyDown(pickUp.keyCode) &&
                collider && collider.GetComponent<ItemCF>())
            {
                MonoBehaviour.Destroy(collider.gameObject);
                pickUp.inventory.AddItem(collider.GetComponent<ItemCF>());
            }
            else if (Input.GetKeyDown(pickUp.keyCode) &&
                collider && collider.GetComponent<OrAliveC>()
                && !collider.GetComponent<OrAliveC>().value)
            {
                MonoBehaviour.Destroy(collider.gameObject);

                pickUp.loot.AddItems(collider.GetComponent<PreyCF>().materials);

                foreach (ItemCF item in collider.GetComponent<PreyCF>().materials)
                {
                    pickUp.inventory.AddItem(item);
                }
            }
        });
    }
}
