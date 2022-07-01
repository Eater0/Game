using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveSettings
{
    public static void SaveSetting(int difficultyLevel, float volume)
    {
        string path;

        BinaryFormatter formatter = new BinaryFormatter();

        path = Application.persistentDataPath + "/settings.survive";

        FileStream stream = new FileStream(path, FileMode.Create);

        DataSettings data = new DataSettings(difficultyLevel, volume);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static DataSettings LoadSetting()
    {
        string path = Application.persistentDataPath + "/settings.survive";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DataSettings data = (DataSettings)formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
