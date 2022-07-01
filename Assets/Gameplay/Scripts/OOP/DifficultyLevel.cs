using UnityEngine;

class DifficultyLevel : MonoBehaviour
{
    AnimalT[] animals;
    GameObject[] gameObjects;
    StaticsCF statics;
    int difficultyLevel;
    int addAttack;
    int addHp;

    public void LoadingTheDifficultyLevel()
    {
        if (difficultyLevel != Manager.GetDifficultyLevel())
        {
            difficultyLevel = Manager.GetDifficultyLevel();

            animals = FindObjectsOfType<AnimalT>();

            gameObjects = new GameObject[animals.Length];

            for (int i = 0; i < animals.Length; i++)
            {
                gameObjects[i] = animals[i].gameObject;
            }

            foreach (var gameObject in gameObjects)
            {
                statics = gameObject.GetComponent<StaticsCF>();
                statics.attack -= addAttack;
                statics.hp -= addHp;
            }

            addAttack = difficultyLevel * 5;
            addHp = difficultyLevel * 5;

            foreach (var gameObject in gameObjects)
            {
                statics = gameObject.GetComponent<StaticsCF>();
                statics.attack += addAttack;
                statics.hp += addHp;
            }
        }
    }
}
