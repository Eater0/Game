using UnityEngine;
using UnityEngine.SceneManagement;

public class RespC : MonoBehaviour
{
    public GameObject current;
    public GameObject doChange;

    GameObject spot;

    void OnDisable()
    {
        if (SceneManager.sceneCount == 1)
        {
            spot = MonoBehaviour.Instantiate(doChange, current.transform.position, current.transform.rotation);

            spot.GetComponent<ID>().id = current.GetComponent<ID>().id;
            spot.GetComponent<ID>().membership = current.GetComponent<ID>().membership;
        }
    }
}
