using UnityEngine;

public class TriggerDamageC : MonoBehaviour
{
    public string tagToAttack;
    public bool distance;
    public int attack;
    public Animator animator;
    public float time;
    public bool stop;

    private void Update()
    {
        time += Time.deltaTime;

        if (time > 0.5f && stop)
        {
            distance = false;
            attack = 0;
            animator = null;

            stop = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals(tagToAttack) && other.GetComponent<StaticsCF>())
        {
            attack = other.GetComponent<StaticsCF>().attack;
            animator = other.GetComponent<Animator>();
            distance = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        time = 0;
        stop = true;
    }
}
