using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerLoad : MonoBehaviour
{
    [SerializeField]
    GameObject template;
    [SerializeField]
    LoadScene loadScene;

    GameObject created;
    Transform button;

    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("data*.survive");

        if (files.Length != 0)
        {
            for (int i = 0; i < files.Length; i++)
            {
                created = Instantiate(template);
                created.transform.SetParent(gameObject.transform, false);

                created.GetComponent<RectTransform>().offsetMax = new Vector2(0, -310 * i);
                created.GetComponent<RectTransform>().sizeDelta = new Vector2(created.GetComponent<RectTransform>().sizeDelta.x, 150);

                Data record = SaveSystem.Load(files[files.Length - 1 - i].Name);

                DataButton(created, record);

                created.transform.GetComponentInChildren<Button>().onClick.AddListener(() => Load(record));
            }
        }
    }

    void DataButton(GameObject created, Data record)
    {
        button = created.transform.GetChild(0);
        button.GetComponentInChildren<TextMeshProUGUI>().text = record.dateDay;
        button.GetChild(1).GetComponent<TextMeshProUGUI>().text = record.dateDate;
    }

    void Load(Data data)
    {
        Manager.SetData(data);
        loadScene.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
