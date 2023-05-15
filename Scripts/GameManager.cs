using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField, Range(20, 60)] public int gameTime = 60;
    [SerializeField, Range(1,3)] private int scorePoints = 2;
    [SerializeField, Range(1,3)] private int backboardBlinkBonus = 4;
    private int perfectThrowBonus = 1;

    public TMP_Text timeText;
    private int winBonus = 25;

    /* Variables used to spawn player and enemy and change their position after each throw */
    private float opponentDistanceFromObjective = 12;
    private float minAngle = 60, maxAngle = 120;
    [SerializeField, Range (10f,25f)]private float opponentsMinDistance = 10f;
    private float playerAngle, opponentAngle;

    private GameObject player;
    private GameObject enemy;
    private GameObject bucket;
    private SceneLoader sceneLoader;
    //private JsonManager jsonManager;
    //private PlayerData playerData;
    private float timeLeft;

    private void Awake()
    {
        
        // Load base game objects
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        bucket = GameObject.FindGameObjectWithTag("Objective");
        if(!player || !enemy || !bucket)
            Debug.LogError("Not found base gameObjects" + player.name + " " +enemy.name +" "+bucket.name);
        
        
        Physics.IgnoreCollision(Utils.GetChildWithName(player, "Pint").GetComponent<SphereCollider>(), 
                                Utils.GetChildWithName(enemy, "Pint").GetComponent<SphereCollider>()
        );
        
        sceneLoader = FindObjectOfType<SceneLoader>();
        timeLeft = (float)gameTime;
    }

    private void Start() 
    {
        RespawnOpponent(player);
        RespawnOpponent(enemy);
    }

    private void Update()
    {   
        // Update the time left
        timeLeft -= Time.deltaTime;
        timeText.text = timeLeft.ToString().Substring(0,4);

        if(timeLeft <= 0){
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
        int playerScore = player.GetComponent<Opponent>().GetScore();
        playerData.playerHasWon = PlayerHasWon();
        if(playerScore > playerData.highestScore)
            playerData.highestScore = playerScore;

        playerData.sessionCoins = playerScore;
        
        // Add the coins to the player based on who won
        if(playerData.playerHasWon)
            playerData.totalCoins = playerData.totalCoins + playerScore + winBonus;
        else playerData.totalCoins = playerData.totalCoins + playerScore;
        
        // Save new data to Json file and load reward scene
        jsonManager.SaveToJson(playerData);
        sceneLoader.LoadScene("Reward");
    }

    public void HandleScoreIncrease(Opponent opponent, float throwerForce, bool isBackboardBlinking)
    {  
        // ()! Calculating the points scored, based on combo and backboard
        int scoreIncrease;
        if(isBackboardBlinking)
            scoreIncrease = scorePoints * opponent.GetScoreMultiplier() + backboardBlinkBonus;
        else scoreIncrease =  scorePoints * opponent.GetScoreMultiplier();
        
        // !!!! Hardcoded make them global
        if(throwerForce >0.40 && throwerForce < 0.43f)
            scoreIncrease += perfectThrowBonus;
        
        
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
        //use the line renderer to determine the correct direction to score a backboard strike, it varies depending on the position
                                                //Pass that direction instead of the objective fixed direction
        Utils.LookAtLockedY(opponent.transform, bucket.transform);
    }

    /* Calculates the new position of the opponent */
    public void PositionOpponent(GameObject opponent)
    {
        float angle = Random.Range(minAngle, maxAngle); 
                                                            
        Vector3 newPosition = bucket.transform.position + 
                                Quaternion.Euler(0, angle, 0) * Vector3.forward * opponentDistanceFromObjective;
        newPosition.y  = 5f;
        Debug.Log("Position :" + newPosition + "Angle  :" + angle);       
        opponent.transform.position = newPosition;
    }
    
}
