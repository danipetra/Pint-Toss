// TOREFACTOR!

using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
    private void Start()
    {
        PlayerData playerData = new PlayerData();
        initializeJson(playerData);
        string data = File.ReadAllText(Application.persistentDataPath + "/playerData.json");
    }

    private void initializeJson(PlayerData playerData)
    {
        if (!File.Exists(Application.persistentDataPath + "/playerData.json"))
        {
            saveToJson(playerData, Application.persistentDataPath + "/playerData.json");
        }
    }

    private void saveInventory(int coinsNumber)
    {
        PlayerData playerData = loadJson(Application.persistentDataPath + "/playerData.json");
        //TODO LoadInventoryChanges 
        saveToJson(playerData, Application.persistentDataPath + "/playerData.json");
    }

    
    public void saveToJson(PlayerData playerData,string jsonPath)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(jsonPath, json);
    }

    public PlayerData loadJson(string jsonPath)
    {
        string json = File.ReadAllText(jsonPath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }
}
