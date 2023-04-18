using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Range(20f, 60f)]
    public float gameTime = 60f;

    [Range(1,3)]
    public int scorePoints = 2;
    public TMP_Text timeText;

    private GameObject player;
    private GameObject enemy;
    private SceneLoader sceneLoader;
    private float timeLeft;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        sceneLoader = GetComponent<SceneLoader>();
        timeLeft = gameTime;
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
        opponent.GetComponent<Opponent>().score += scorePoints * opponent.GetComponent<Opponent>().scoreMultiplier;
        opponentText.text = opponent.GetComponent<Opponent>().score.ToString();
    } 

    private void ChooseWinner(){
        if(player.GetComponent<Opponent>().score >= enemy.GetComponent<Opponent>().score){
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
}
