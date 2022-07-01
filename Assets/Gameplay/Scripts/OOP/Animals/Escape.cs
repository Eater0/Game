using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;

class Escape : Agent
{
    public Transform player;

    CharacterController controller;

    Vector3 direction;
    Vector3 forwardV;
    Vector3 velocityY;

    public override void Initialize()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        direction = player.position - transform.position;

        sensor.AddObservation(direction);
        sensor.AddObservation(transform.forward);
        sensor.AddObservation(Vector3.Dot(transform.forward.normalized, direction.normalized));

        sensor.AddObservation(transform.rotation.y);
        sensor.AddObservation(transform.rotation.w);

        sensor.AddObservation(GetComponent<Rigidbody>().velocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float angle = Mathf.Clamp(actions.ContinuousActions[0], -1, 1);

        transform.localRotation *= Quaternion.Euler(0, 300 * Time.deltaTime * angle, -transform.localRotation.eulerAngles.z);

        if (controller.isGrounded && velocityY.y < 0)
        {
            velocityY.y = -2;
        }

        velocityY.y += -9.81f * Time.deltaTime;

        forwardV = transform.forward;
        forwardV.y = 0;

        controller.Move(forwardV * 8 * Time.deltaTime + velocityY * Time.deltaTime);

        /*SetReward(-Vector3.Dot(transform.forward.normalized, direction.normalized));
        SetReward(0.1f);*/
    }

    /*public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
        transform.localRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);

        player.localPosition = new Vector3(Random.Range(-40, 40), 1, Random.Range(-40, 40));
        player.localRotation = Quaternion.Euler(0, Random.Range(-180, 180), 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            SetReward(-14);
            EndEpisode();
        }
        else if (!other.tag.Equals("Terrain"))
        {
            SetReward(-1);
            EndEpisode();
        }
    }*/
}
