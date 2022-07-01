using UnityEngine;

class IsGrounded : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        gameObject.transform.parent.GetComponent<NCIsGroundedC>().value = true;
    }

    void OnTriggerExit(Collider other)
    {
        gameObject.transform.parent.GetComponent<NCIsGroundedC>().value = false;
    }
}
