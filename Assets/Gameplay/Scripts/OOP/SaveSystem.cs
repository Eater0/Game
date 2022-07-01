using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void Save(List<GameObject> revivalEnviroment, List<GameObject> revivalCreatures, GameObject[] creatures, GameObject[] buildings, GameObject character, List<Item> itemsInInventory, ItemCF[] armament)
    {
        string path;

        BinaryFormatter formatter = new BinaryFormatter();

        DirectoryInfo dir = new DirectoryInfo(Application.persistentDataPath);
        FileInfo[] files = dir.GetFiles("data*.survive");

        if (files.Length == 0)
        {
            path = Application.persistentDataPath + "/data0.survive";
        }
        else if (files.Length < 10)
        {
            path = Application.persistentDataPath + 
                files[0].Name.
                Insert(0, "/").
                Remove(5, 1).
                Insert(5, files.Length.ToString());
        }
        else
        {
            File.Delete(files[0].FullName);

            for (int i = 0; i < files.Length - 1; i++)
            {
                files[i] = files[i + 1];

                File.Move(files[i].FullName,
                    files[i].FullName.
                    Remove(files[i].FullName.Length - 9, 1).
                    Insert(files[i].FullName.Length - 9, i.ToString()));
            }

            path = Application.persistentDataPath + "/data9.survive";
        }

        FileStream stream = new FileStream(path, FileMode.Create);

        Data data = new Data(revivalEnviroment, revivalCreatures, creatures, buildings, character, itemsInInventory, armament);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Data Load(string nameFile)
    {
        string path = Application.persistentDataPath + "/" + nameFile;

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Data data = (Data) formatter.Deserialize(stream);
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }
}
