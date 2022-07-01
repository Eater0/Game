using UnityEngine;

public class Manager : MonoBehaviour
{
    public static int diffucltyLevel = 1;
    public static Data data;

    public static void SetDiffcultyLevel(int diffucltyLevell)
    {
        diffucltyLevel = diffucltyLevell;
    }

    public static int GetDifficultyLevel()
    {
        return diffucltyLevel;
    }

    public static void SetData(Data dataa)
    {
        data = dataa;
    }

    public static Data GetData()
    {
        return data;
    }
}
