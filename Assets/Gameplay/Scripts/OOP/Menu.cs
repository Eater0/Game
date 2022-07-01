using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    PuttingOnArmament PuttingOnArmament;
    [SerializeField]
    GameObject save;

    RevivalC[] revivals;
    AnimalT[] animals;
    BuildingT[] buildingTs;

    public void Resume()
    {
        gameObject.SetActive(false);

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Save()
    {
        List<GameObject> revivalEnviroment = new List<GameObject>();
        List<GameObject> revivalCreatures = new List<GameObject>();

        revivals = FindObjectsOfType<RevivalC>();

        foreach (var revival in revivals)
        {
            if (revival.gameObject.GetComponent<AnimalT>())
            {
                revivalCreatures.Add(revival.gameObject);
            }
            else
            {
                revivalEnviroment.Add(revival.gameObject);
            }
        }


        animals = FindObjectsOfType<AnimalT>();
        GameObject[] creatures = new GameObject[animals.Length];

        for (int i = 0; i < animals.Length; i++)
        {
            creatures[i] = animals[i].gameObject;
        }

        buildingTs = FindObjectsOfType<BuildingT>();
        GameObject[] buildings = new GameObject[buildingTs.Length];

        for (int i = 0; i < buildingTs.Length; i++)
        {
            buildings[i] = buildingTs[i].gameObject;
        }

        try
        {
            SaveSystem.Save(revivalEnviroment, revivalCreatures, creatures, buildings, GameObject.FindGameObjectWithTag("Player"), inventory.GetItems(), PuttingOnArmament.GetCurrentItems());
            StartCoroutine(View());
        }
        catch { }
    }

    public void ExitToTheMenu()
    {
        Manager.SetData(null);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void ExitToTheGame()
    {
        Application.Quit();
    }

    IEnumerator View()
    {
        save.SetActive(true);
        yield return new WaitForSecondsRealtime(2);
        save.SetActive(false);
    }
}
