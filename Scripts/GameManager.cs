using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(20, 60)] public int gameTime = 60;
    [SerializeField, Range(1,3)] private int _scorePoints = 2;
    [SerializeField, Range(1,3)] private int _backboardBlinkBonus = 4;
    private int _perfectThrowBonus = 1;

    public TMP_Text timeText;
    private int _winBonus = 25;

    /* Variables used to spawn player and enemy and change their position after each throw */
    private float _opponentDistanceFromObjective = 12;
    private float _minAngle = 60, _maxAngle = 120;
    [SerializeField, Range (10f,25f)]private float _opponentsMinDistance = 10f;
    private float _playerAngle, _opponentAngle;

    private GameObject _player;
    private GameObject _enemy;
    private GameObject _bucket;
    private SceneLoader _sceneLoader;
    //private JsonManager jsonManager;
    //private PlayerData playerData;
    private float _timeLeft;

    private void Awake()
    {
        
        // Load base game objects
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemy = GameObject.FindGameObjectWithTag("Enemy");
        _bucket = GameObject.FindGameObjectWithTag("Objective");
        if(!_player || !_enemy || !_bucket)
            Debug.LogError("Not found base gameObjects" + _player.name + " " +_enemy.name +" "+_bucket.name);
        
        
        Physics.IgnoreCollision(Utils.GetChildWithName(_player, "Pint").GetComponent<SphereCollider>(), 
                                Utils.GetChildWithName(_enemy, "Pint").GetComponent<SphereCollider>()
        );
        
        _sceneLoader = FindObjectOfType<SceneLoader>();
        _timeLeft = (float)gameTime;
    }

    private void Start() 
    {
        RespawnOpponent(_player);
        RespawnOpponent(_enemy);
    }

    private void Update()
    {   
        // Update the time left
        _timeLeft -= Time.deltaTime;
        timeText.text = _timeLeft.ToString().Substring(0,4);

        if(_timeLeft <= 0){
            StopGame();
        }
    }

    /* Function called at the game end, saves the game data and loads the Gameover scene */
    private void StopGame()
    {
        // Load previous player data
        JsonManager jsonManager = gameObject.AddComponent<JsonManager>();
        PlayerData playerData = jsonManager.LoadJson();
        // Update data
        int playerScore = _player.GetComponent<Opponent>().GetScore();
        playerData.playerHasWon = PlayerHasWon();
        if(playerScore > playerData.highestScore)
            playerData.highestScore = playerScore;

        playerData.sessionCoins = playerScore;
        
        // Add the coins to the player based on who won
        if(playerData.playerHasWon)
            playerData.totalCoins = playerData.totalCoins + playerScore + _winBonus;
        else playerData.totalCoins = playerData.totalCoins + playerScore;
        
        // Save new data to Json file and load reward scene
        jsonManager.SaveToJson(playerData);
        _sceneLoader.LoadScene("Reward");
    }

    public void HandleScoreIncrease(Opponent opponent, float throwerForce, bool isBackboardBlinking)
    {  
        // ()! Calculating the points scored, based on combo and backboard
        int scoreIncrease;
        if(isBackboardBlinking)
            scoreIncrease = _scorePoints * opponent.GetScoreMultiplier() + _backboardBlinkBonus;
        else scoreIncrease =  _scorePoints * opponent.GetScoreMultiplier();
        
        // !!!! Hardcoded make them global
        if(throwerForce >0.40 && throwerForce < 0.43f)
            scoreIncrease += _perfectThrowBonus;
        
        
        opponent.IncreaseScore(scoreIncrease);
        
        // Updating the opponent UI elements
        opponent.scoreText.text = opponent.GetScore().ToString();
        if(opponent.gameObject.CompareTag("Player"))
            opponent.gameObject.GetComponent<Player>().ShowPoints(scoreIncrease); 

    } 

    private int CalculateScore(Opponent opponent)
    {
        return 0;
    }

    private bool PlayerHasWon()
    {
        if(_player.GetComponent<Opponent>().GetScore() >= _enemy.GetComponent<Opponent>().GetScore())
            return true;
        
        else return false;
    }

    public void RespawnOpponent(GameObject opponent)
    {
        if(opponent.tag == "Player") 
            opponent.GetComponent<Player>().PickPint();
        else 
            opponent.GetComponent<Enemy>().PickPint();
        
        //PositionOpponent(opponent);
        
        // TODO 
        //use the line renderer to determine the correct direction to score a backboard strike, it varies depending on the position
                                                //Pass that direction instead of the objective fixed direction
        Utils.LookAtLockedY(opponent.transform, _bucket.transform);
    }

    /* Calculates the new position of the opponent */
    public void PositionOpponent(GameObject opponent)
    {
        float angle = Random.Range(_minAngle, _maxAngle); 
                                                            
        Vector3 newPosition = _bucket.transform.position + 
                                Quaternion.Euler(0, angle, 0) * Vector3.forward * _opponentDistanceFromObjective;
        newPosition.y  = 5f;
        Debug.Log("Position :" + newPosition + "Angle  :" + angle);       
        opponent.transform.position = newPosition;
    }
    
}
