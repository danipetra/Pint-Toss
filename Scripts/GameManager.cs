using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [Range(20, 60)]public int gameTime = 60;
    [Range(1,3)]public int scorePoints = 2;
    public TMP_Text timeText;

    /* Variables used to spawn player and enemy and change their position after each throw*/
    [SerializeField, Range(5f, 12f)]private float opponentDistanceFromObjective = 5f;
    private float minAngle = 10, maxAngle = 170;
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
        bucket = GameObject.FindGameObjectWithTag("Objective");
        sceneLoader = GetComponent<SceneLoader>();
        timeLeft = (float)gameTime;
    }

    private void Start() {
        RespawnOpponent(player);
        RespawnOpponent(enemy);
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(GameTimeEnded())
            StopGame();

        timeText.text = timeLeft.ToString().Substring(0,4);
    }

    bool GameTimeEnded(){
        if(gameTime <= 0){
            return true;
        }
        return false;  
    }

    void StopGame(){
        //Debug.Log("Game Over!");
        ChooseWinner();
    }

    public void AddScore(GameObject opponent, TMP_Text opponentText){  
        opponent.GetComponent<Opponent>().IncreaseScore(scorePoints * opponent.GetComponent<Opponent>().GetScoreMultiplier());
        opponentText.text = opponent.GetComponent<Opponent>().GetScore().ToString();
    } 

    private void ChooseWinner(){
        if(player.GetComponent<Opponent>().GetScore() >= enemy.GetComponent<Opponent>().GetScore()){
            Debug.LogWarning("MISSING Implement Win Scene!");
            AddMoneyToPlayer();
            //sceneLoader.LoadScene("WinScene");
        }
        else{
            Debug.LogWarning("MISSING Implement Lose Scene!");
            //sceneLoader.LoadScene("LoseScene");
        }
    }
    
    private void AddMoneyToPlayer(){
        Debug.LogWarning("MISSING Add money to player function!");
    } 

    public void RespawnOpponent(GameObject opponent){
        if(opponent.name == "Player") opponent.GetComponent<Player>().Respawn(); else opponent.GetComponent<Opponent>().Respawn();
        //Assign the player a position
            //Base it on a rotation around the bucket
        Quaternion rotation = Quaternion.Euler(0, Random.Range(minAngle , maxAngle), 0);

        //Vector3 respawnPosition = bucket.transform + Random.insideUnitCircle * radius * 0.5f;
        // fix the y respawnPosition
        
        Utils.LookAtLockedY(opponent.transform, bucket.transform);
    }
    
}
