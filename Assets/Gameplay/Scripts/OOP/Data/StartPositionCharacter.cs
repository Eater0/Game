using UnityEngine;

public class StartPositionCharacter : MonoBehaviour
{
    GameObject character;

    void FixedUpdate()
    {
        character = GameObject.FindGameObjectWithTag("Player");

        Data data = Manager.GetData();

        character.transform.position = new Vector3(data.character.position.x, data.character.position.y, data.character.position.z);
        character.transform.rotation = new Quaternion(data.character.rotation.x, data.character.rotation.y, data.character.rotation.z, data.character.rotation.w);

        Destroy(gameObject.GetComponent<StartPositionCharacter>());
    }
}
