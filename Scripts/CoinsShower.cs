using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text coinsText;
    public TMP_Text totalCoinsText;
    public TMP_Text hiscoreText;
    public TMP_Text winnerText;
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
        if (hiscoreText)
        {
            UpdateHiscore();
        }
        if (winnerText)
        {
            UpdateWinnerText();
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

    private void UpdateHiscore()
    {
        hiscoreText.text = playerData.hiscoreText.ToString();
    }

    private void UpdateWinnerText(bool playerHasWon)
    {
        if(playerHasWon)
            winnerText.text = "You won!";
        else winnerText.text = "You loose!";
    }

}

