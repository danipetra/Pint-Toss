using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text coinsText;
    public TMP_Text totalCoinsText;
    //public Button rewardedAdButton;

    private JsonManager jsonManager;
    private PlayerData playerData;

   
    private void Awake()
    {
        jsonManager = new JsonManager();
        playerData = jsonManager.LoadJson(Application.persistentDataPath + "/playerData.json");
    }

    private void Update()
    {
        //depending on scene update text fields
        if (coinsText)
        {
            UpdateSessionCoins();
        }
        if (totalCoinsText)
        {
            UpdateTotalCoins();
        }
    }

    private void UpdateSessionCoins()
    {
        coinsText.text = playerData.sessionCoins.ToString();
    }

    private void UpdateTotalCoins()
    {
        totalCoinsText.text = playerData.totalCoins.ToString();
    }

    /*public void giveExtraCoins()
    {
        adsManager.PlayRewardedAd(onRewardedAdSuccess);

        PlayerData playerData = jsonManager.LoadJson(Application.persistentDataPath + "/playerData.json");
        UpdateTotalCoins();

        rewardedAdButton.interactable = false;
    }

    public void onRewardedAdSuccess()
    {
        PlayerData playerData = jsonManager.LoadJson(Application.persistentDataPath + "/playerData.json");

        playerData.coins += playerData.sessionCoins;

        totalCoinAmount = playerData.coins;

        playerData.sessionCoins = 0;

        jsonManager.SaveToJson(playerData,Application.persistentDataPath + "/playerData.json");
    }*/
}

