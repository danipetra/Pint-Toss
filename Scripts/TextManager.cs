using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    public TMP_Text coinsText;
    public TMP_Text totalCoinsText;
    public TMP_Text hiscoreText;
    public TMP_Text winnerText;
    //public Button rewardedAdButton;

    private JsonManager _jsonManager;
    private PlayerData _playerData;

    private void Start()
    {
        _jsonManager = gameObject.AddComponent<JsonManager>();
        _playerData = _jsonManager.LoadJson();
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
        coinsText.text = _playerData.sessionCoins.ToString();
    }

    private void UpdateTotalCoins()
    {
        totalCoinsText.text = _playerData.totalCoins.ToString();
    }

    private void UpdateHiscore()
    {
        hiscoreText.text = _playerData.highestScore.ToString();
    }

    private void UpdateWinnerText()
    {
        if(_playerData.playerHasWon)
            winnerText.text = "You win!";
        else winnerText.text = "You loose!";
    }

}

