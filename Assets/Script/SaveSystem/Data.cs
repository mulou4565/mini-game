using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Data : MonoBehaviour
{
    const string DATA_FILE_NAME = "Data.cundang";
    public Vector3 StartPosition;
    public int SaveScene;
    [System.Serializable]class SaveData
    {
        public Vector3 Position;
        public int SceneNum;
    }
    SaveData SavingData()
    {
        var saveData = new SaveData();

        //saveData.Position = transform.position;
        saveData.SceneNum = SceneManager.GetActiveScene().buildIndex;

        return saveData;
    }
    void LoadingData(SaveData saveData)
    {
        //SceneManager.LoadScene(saveData.SceneNum);
        //transform.position = saveData.Position;
        SaveScene=saveData.SceneNum;
        Debug.Log(saveData.Position);
    }
    public void Save()
    {
        SaveByJson();
    }

    public void Load()
    {
        LoadFromJson();
    }

    void SaveByJson()
    {
        SaveSystem.SaveByJson(DATA_FILE_NAME, SavingData());

    }

    void LoadFromJson()
    {
        var saveData = SaveSystem.LoadFromJson<SaveData>(DATA_FILE_NAME);
        LoadingData(saveData);
    }

    public static void DeleteDataSaveFile()
    {
        SaveSystem.DeleteSaveFile(DATA_FILE_NAME);
    }

    public void CreateNewSave()
    {
        transform.position = StartPosition;
        SceneManager.LoadScene(1);
    }
}
