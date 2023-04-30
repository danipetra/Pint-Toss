// TOREFACTOR!

using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
                                    // Application.persistentDataPath, + "/playerData.json";
    public string saveDataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
    
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
        File.WriteAllText(saveDataPath, json);
    }

    public PlayerData LoadJson(string jsonPath)
    {
        string json = File.ReadAllText(saveDataPath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }
}
