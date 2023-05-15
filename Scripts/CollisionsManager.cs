using UnityEngine;

public class CollisionsManager : MonoBehaviour
{
    [SerializeField] private GameObject _bucket, _backboard;
    private GameManager _gameManager;
    private AudioManager _audioManager;

    private void Awake() 
    {
        _gameManager = GetComponent<GameManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    public void HandleBackboardCollision(Pint pint, Opponent opponent)
    {
        _audioManager.Play("Bounce");
        _audioManager.Play("Backboard");

        opponent.SetScoreMultiplier(opponent.GetScoreMultiplier() * 2);
        pint.hitBackboard = true;

        if(_backboard.GetComponent<Backboard>().IsBlinking())
            pint.hasBackboardBlinkBonus = true;
    }

    public void HandleFloorCollision(Pint pint, Opponent opponent)
    {
        _audioManager.Play("Bounce");

        opponent.SetComboBarValue(0);
        if(opponent.GetScoreMultiplier() > 1)
                    opponent.SetScoreMultiplier(opponent.GetScoreMultiplier() / 2);

        _gameManager.RespawnOpponent(opponent.gameObject);

        if(pint.hitBackboard)
        {
            pint.hitBackboard = false;
            pint.hasBackboardBlinkBonus = false;
                
                if(opponent.GetScoreMultiplier() > 1)
                    opponent.SetScoreMultiplier(opponent.GetScoreMultiplier() / 2);
            
        }
    }

    public void HandleBucketCollision(Pint pint, Opponent opponent)
    {
        _audioManager.Play("Bounce");
        _audioManager.Play("Score");

        opponent.SetComboBarValue(opponent.GetComboValue() + 1 );
            
        if(pint.hitBackboard && opponent.GetScoreMultiplier() > 1)
            opponent.SetScoreMultiplier(opponent.GetScoreMultiplier() / 2);

        _gameManager.RespawnOpponent(opponent.gameObject);
    }
}
