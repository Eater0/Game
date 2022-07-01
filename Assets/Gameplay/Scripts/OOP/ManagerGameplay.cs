using UnityEngine;

class ManagerGameplay : MonoBehaviour
{
    [SerializeField]
    GameObject rucksack;
    [SerializeField]
    GameObject crafting;
    [SerializeField]
    GameObject loot;
    [SerializeField]
    GameObject menu;

    [Header("NeedLoad")]
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    ItemCF[] items;
    [SerializeField]
    GameObject[] buildings;
    [SerializeField]
    GameObject[] armament;

    [SerializeField]
    PuttingOnArmament puttingOnArmament;
    [SerializeField]
    GameObject character;

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;

        menu.SetActive(false);
        rucksack.SetActive(false);
        crafting.SetActive(false);
        loot.SetActive(false);

        Time.timeScale = 1;

        DifficultyLevel difficulty = new DifficultyLevel();
        difficulty.LoadingTheDifficultyLevel();

        if (Manager.GetData() != null)
        {
            Load load = new Load();
            load.LoadSave(inventory, items, buildings, puttingOnArmament, character, armament);

            gameObject.AddComponent<StartPositionCharacter>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(!menu.activeSelf);
            rucksack.SetActive(false);
            crafting.SetActive(false);

            ViewCursorAndTime();
        }

        if (Input.GetKeyDown(KeyCode.I) && !crafting.activeSelf)
        {
            rucksack.SetActive(!rucksack.activeSelf);

            ViewCursorAndTime();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            crafting.SetActive(!crafting.activeSelf);
            rucksack.SetActive(crafting.activeSelf);

            ViewCursorAndTime();
        }
    }

    void ViewCursorAndTime()
    {
        if (rucksack.activeSelf || crafting.activeSelf || menu.activeSelf)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
