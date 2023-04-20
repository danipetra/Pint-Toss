using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    [Range(20, 60)]public int gameTime = 60;
    [Range(1,3)]public int scorePoints = 2;
    public TMP_Text timeText;

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
        //SpawnOpponent(player);
        //SpawnOpponent(enemy);
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

    } 

    public void SpawnOpponent(GameObject Opponent){
        
        int randomAngle = Random.Range(-165, 165);
        
        //Rotate the opponent around the bucket y axis for a angle value
        Opponent.transform.Rotate(bucket.transform.position, randomAngle, Space.World);
        // Rotate him to face the bucket

        // Rotate the camera according to that
    }   
    
}
