using System;
using System.IO;
using Unity.Entities;
using UnityEngine;

public partial class PersistenceSubsystem : EngineSubsystemBase
{
    protected override void OnCreate()
    {
    }

    protected override void OnUpdate()
    {
        
    }
    
    public void SaveToFile<T>(string filePath, T data) where T : SaveDataBase
    {
        string path = Path.Combine(Application.persistentDataPath, filePath);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);
    }

    public T LoadFromFile<T>(string filePath) where T : SaveDataBase, new()
    {
        string path = Path.Combine(Application.persistentDataPath, filePath);
        
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<T>(json);
        }

        return new T();
    }
}
