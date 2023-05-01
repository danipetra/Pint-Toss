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

    private void Start()
    {
        jsonManager = gameObject.AddComponent<JsonManager>();
        playerData = jsonManager.LoadJson();
    }

    private void Update()
    {
        //depending on scene update text fields
        if (coinsText)
            UpdateSessionCoins();
        
        if (totalCoinsText)
            UpdateTotalCoins();
        
        if (hiscoreText)
            UpdateHiscore();
        
        if(winnerText)
            UpdateWinnerText();
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
        hiscoreText.text = playerData.highestScore.ToString();
    }

    private void UpdateWinnerText()
    {
        if(playerData.playerHasWon)
            winnerText.text = "You win!";
        else winnerText.text = "You loose!";
    }

}

