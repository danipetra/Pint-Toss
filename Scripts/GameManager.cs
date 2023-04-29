using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [SerializeField, Range(20, 60)] public int gameTime = 60;
    [SerializeField, Range(1,3)] private int scorePoints = 2;
    [SerializeField, Range(1,3)] private int backboardBlinkBonus = 4;

    public TMP_Text timeText;
    private int winBonus = 25;

    /* Variables used to spawn player and enemy and change their position after each throw */
    private float opponentDistanceFromObjective = 12;
    private float minAngle = 60, maxAngle = 120;
    [SerializeField, Range (10f,25f)]private float minDistanceBetweenOpponents = 10f;
    private float playerAngle, opponentAngle;

    private GameObject player;
    private GameObject enemy;
    private GameObject bucket;
    private SceneLoader sceneLoader;
    private float timeLeft;

    private void Awake()
    {
        // Load base game objects
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        bucket = GameObject.FindGameObjectWithTag("Objective");
        if(!player || !enemy || !bucket)
            Debug.LogError("Not found base gameObjects" + player.name + " " +enemy.name +" "+bucket.name);
        
        
        Physics.IgnoreCollision(player.GetComponentInChildren<SphereCollider>(), enemy.GetComponentInChildren<SphereCollider>());
        sceneLoader = FindObjectOfType<SceneLoader>();
        timeLeft = (float)gameTime;
    }

    private void Start() 
    {
        RespawnOpponent(player);
        RespawnOpponent(enemy);
    }

    void Update()
    {   
        // Update the time left
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString().Substring(0,4);

        if(timeLeft <= 0){
            StopGame();
        }
    }

    /* Function called at the game end, saves the game data and loads the Gameover scene */
    void StopGame()
    {
        // Load previous player data
        JsonManager jsonManager = new JsonManager();
        PlayerData playerData = jsonManager.LoadJson(jsonManager.saveDataPath);
        
        // Update data
        int playerScore = player.GetComponent<Opponent>().GetScore();
        playerData.sessionCoins = playerScore;
        playerData.playerHasWon = PlayerHasWon();
        if(playerScore > playerData.highestScore)
            playerData.highestScore = playerScore;

        // Add the coins to the player based on who won
        if(playerData.playerHasWon)
            playerData.totalCoins = playerData.totalCoins + playerScore + winBonus;
        else playerData.totalCoins = playerData.totalCoins + playerScore;
        
        jsonManager.SaveToJson(playerData, jsonManager.saveDataPath);
        sceneLoader.LoadScene("Reward");
    }

    public void UpdateScore(GameObject opponent, bool isBackboardBlinking)
    {  
        int scoreIncrease;
        if(isBackboardBlinking)
            scoreIncrease = scorePoints * opponent.GetComponent<Opponent>().GetScoreMultiplier() + backboardBlinkBonus;
        else scoreIncrease =  scorePoints * opponent.GetComponent<Opponent>().GetScoreMultiplier();
        
        opponent.GetComponent<Opponent>().IncreaseScore(scoreIncrease);
        opponent.GetComponent<Opponent>().scoreText.text = opponent.GetComponent<Opponent>().GetScore().ToString();
    } 

    private bool PlayerHasWon()
    {
        if(player.GetComponent<Opponent>().GetScore() >= enemy.GetComponent<Opponent>().GetScore())
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
        //use the raycast to determine the correct direction to score a backboard strike, it varies depending on the position
                                                //Pass that direction instead of the objective fixed direction
        Utils.LookAtLockedY(opponent.transform, bucket.transform);
    }

    /* Calculates the new position of the opponent */
    public void PositionOpponent(GameObject opponent)
    {
        float angle = Random.Range(minAngle, maxAngle); 
                                                            
        Vector3 newPosition = bucket.transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * opponentDistanceFromObjective;
        newPosition.y  = 5f;
        Debug.Log("Position :" + newPosition + "Angle  :" + angle);       
        opponent.transform.position = newPosition;
    }
    
}
