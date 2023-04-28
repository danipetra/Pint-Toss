// TOREFACTOR!

using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
    public string saveDataPath = Application.persistentDataPath + "/playerData.json";
    
    private void Awake()
    {
        PlayerData playerData = new PlayerData();
        InitializeJson(playerData);
        string data = File.ReadAllText(saveDataPath);
    }

    private void InitializeJson(PlayerData playerData)
    {
        if (!File.Exists(saveDataPath))
            SaveToJson(playerData, saveDataPath);
    }

    public void SaveToJson(PlayerData playerData, string jsonPath)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(jsonPath, json);
    }

    public PlayerData LoadJson(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }
}
