using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [Range(20, 60)]public int gameTime = 60;
    [Range(1,3)]public int scorePoints = 2;
    public TMP_Text timeText;
    private int winBonus = 30;

    /* Variables used to spawn player and enemy and change their position after each throw*/
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
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        Physics.IgnoreCollision(player.GetComponentInChildren<SphereCollider>(), enemy.GetComponentInChildren<SphereCollider>());

        bucket = GameObject.FindGameObjectWithTag("Objective");
        if(!player || !enemy || !bucket)
            Debug.LogError("Not found base gameObjects" + player.name + " " +enemy.name +" "+bucket.name);
        
        sceneLoader = FindObjectOfType<SceneLoader>();
        timeLeft = (float)gameTime;
    }

    private void Start() {
        RespawnOpponent(player);
        RespawnOpponent(enemy);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft <= 0){
            StopGame();
        }
        timeText.text = timeLeft.ToString().Substring(0,4);
    }

    void StopGame(){
        JsonManager jsonManager = new JsonManager();
        PlayerData playerData = jsonManager.LoadJson(jsonManager.saveDataPath);
        Debug.Log("prev coins : "+ playerData.totalCoins);

        int playerScore = player.GetComponent<Opponent>().GetScore();
        
        bool playerHasWon = PlayerHasWon();
        if(playerHasWon){
            jsonManager.AddCoins(playerScore + winBonus);
        }
        else{
            jsonManager.AddCoins(playerScore);
        }
        playerData = jsonManager.LoadJson(jsonManager.saveDataPath);
        Debug.Log("new coins total: " + playerData.totalCoins);
        Debug.Log("new sesssion coins: " + playerData.totalCoins +" " + playerData.sessionCoins);
        sceneLoader.LoadScene("Reward", playerHasWon);
    }

    public void AddScore(GameObject opponent, TMP_Text opponentText){  
        opponent.GetComponent<Opponent>().IncreaseScore(scorePoints * opponent.GetComponent<Opponent>().GetScoreMultiplier());
        opponentText.text = opponent.GetComponent<Opponent>().GetScore().ToString();
    } 

    private bool PlayerHasWon(){
        if(player.GetComponent<Opponent>().GetScore() >= enemy.GetComponent<Opponent>().GetScore())
            return true;
        
        else return false;
    }

    public void RespawnOpponent(GameObject opponent){
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

    /* Calculate the new position of the opponent */
    public void PositionOpponent(GameObject opponent){
        float angle = Random.Range(minAngle, maxAngle); 
                                                            
        Vector3 newPosition = bucket.transform.position + Quaternion.Euler(0, angle, 0) * Vector3.forward * opponentDistanceFromObjective;
        newPosition.y  = 5f;
        Debug.Log("Position :" + newPosition + "Angle  :" + angle);       
        opponent.transform.position = newPosition;
    }
    
}
