using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

class ManagerMenu : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    LoadScene loadScene;

    DontDestroyGameObjectOnLoad[] music;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;

        music = GameObject.FindObjectsOfType<DontDestroyGameObjectOnLoad>();

        if (music.Length == 2)
        {
            Destroy(music[1].gameObject);
        }
    }

    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("settings.survive");

        if (files.Length == 1)
        {
            DataSettings data = SaveSettings.LoadSetting();

            Manager.SetDiffcultyLevel(data.difficultyLevel);
            audioMixer.SetFloat("sound", data.volume);
        }
    }

    public void Continue()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("data*.survive");

        if (files.Length > 0)
        {
            Data record = SaveSystem.Load(files[files.Length - 1].Name);
            Manager.SetData(record);
        }

        loadScene.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGameplay()
    {
        loadScene.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
