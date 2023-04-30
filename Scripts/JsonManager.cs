// TOREFACTOR!

using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
                                    // Path.Combine(Application.persistentDataPath, "playerData.json");
    //public string saveDataPath = Application.persistentDataPath + "/playerData.json";
    
    private void Awake()
    {
        PlayerData playerData = new PlayerData();
        InitializeJson(playerData);
        PlayerData data = LoadJson(Application.persistentDataPath + "/playerData.json");
    }

    private void InitializeJson(PlayerData playerData)
    {
        if (!File.Exists(Application.persistentDataPath + "/playerData.json")) {
            playerData.level = 0;
            playerData.totalCoins = 0;
            playerData.sessionCoins = 0;
            playerData.playerHasWon = false;
            playerData.highestScore = 0;
            SaveToJson(playerData, Application.persistentDataPath + "/playerData.json");
        }   
    }

    public void SaveToJson(PlayerData playerData, string jsonPath)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(Application.persistentDataPath + "/playerData.json", json);
    }

    public PlayerData LoadJson(string jsonPath)
    {
        string json = File.ReadAllText(Application.persistentDataPath + "/playerData.json");
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }
}
