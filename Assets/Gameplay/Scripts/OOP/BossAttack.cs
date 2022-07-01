using UnityEngine;

public class BossAttack : MonoBehaviour
{

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameObject.transform.parent.
                parent.
                parent.
                parent.
                parent.
                parent.
                parent.
                parent.GetComponent<NCBossAttackC>().value = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            gameObject.transform.parent.
                parent.
                parent.
                parent.
                parent.
                parent.
                parent.
                parent.GetComponent<NCBossAttackC>().value = false;
        }
    }
}
