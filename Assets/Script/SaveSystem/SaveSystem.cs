using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class SaveSystem
{
    public static void SaveByJson(string SaveFileName,object data)
    {
        var json = JsonUtility.ToJson(data);
        var path = Path.Combine(Application.persistentDataPath, SaveFileName);

        File.WriteAllText(path,json);

        try
        {
            File.WriteAllText(path, json);
            Debug.Log($"Susscessfullly saved data to {path}.");
        }
        catch (System.Exception exception)
        {
            Debug.LogError($"Failed to save data to {path}.\n{exception}");
        }
    }

    public static T LoadFromJson<T>(string SaveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, SaveFileName);
        try
        {
            var json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<T>(json);
            Debug.Log($"Susscessfullly load data from {path}.");
            return data;
        }
        catch (System.Exception exception)
        {
            Debug.LogError($"Failed to load data from {path}.\n{exception}");
            return default;
        }
    }

    public static void DeleteSaveFile(string SaveFileName)
    {
        var path = Path.Combine(Application.persistentDataPath, SaveFileName);

        try
        {
            File.Delete(path);
        }
        catch (System.Exception exception)
        {
            Debug.LogError($"Failed to delete{path}.\n{exception}");
        }
    }
}
