// TOREFACTOR!

using UnityEngine;
using UnityEngine.Android;
using System.IO;

public class JsonManager : MonoBehaviour
{
    public string saveDataPath = Application.persistentDataPath + "/playerData.json";
    
    private void Awake()
    {
        
        PlayerData playerData = new PlayerData();
        InitializeJson(playerData);
        PlayerData data = LoadJson(saveDataPath);
    }

    private void InitializeJson(PlayerData playerData)
    {
        if (!File.Exists(saveDataPath)) {
            playerData.level = 0;
            playerData.totalCoins = 0;
            playerData.sessionCoins = 0;
            playerData.playerHasWon = false;
            playerData.highestScore = 0;
            SaveToJson(playerData, saveDataPath);
        }   
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
