using UnityEngine;
using System.IO;

public class JsonManager : MonoBehaviour
{
                        
    private string _saveDataPath = "";
    
    private void Awake()
    {
        _saveDataPath = Path.Combine(Application.persistentDataPath, "playerData.json");
        PlayerData playerData = new PlayerData();
        
        InitializeJson(playerData);
        PlayerData data = LoadJson();
    }

    private void InitializeJson(PlayerData playerData)
    {
        if (!File.Exists(_saveDataPath)) {
            playerData.level = 0;
            playerData.totalCoins = 0;
            playerData.sessionCoins = 0;
            playerData.playerHasWon = false;
            playerData.highestScore = 0;
            
            SaveToJson(playerData);
        }   
    }

    public void SaveToJson(PlayerData playerData)
    {
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(_saveDataPath, json);
    }

    public PlayerData LoadJson()
    {
        string json = File.ReadAllText(_saveDataPath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
        return playerData;
    }
}
