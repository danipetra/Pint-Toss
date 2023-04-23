using UnityEngine;
using TMPro;
using System;

public class Opponent : MonoBehaviour
{
    [Range(3f,5f)] public float fireDuration;
    // TODO make private
    public TMP_Text scoreText;
    
    [SerializeField, Range(10, 25)]public int force; //TODO change it to protected
    [SerializeField, Range(0.5f, 2f)] public float maximumTime = 1.2f;
    [SerializeField, Range(2, 5)]private int yForceDividend;
    [SerializeField]private int score;
    private bool isOnFire;
    private int comboValue;
    private int scoreMultiplier;
    private int yForce;
    private GameObject pint;

    protected void Start()
    {   
        //InitVariables()
        scoreText.text = score.ToString();
        scoreMultiplier = 1;
        isOnFire = false;
        comboValue = 0;
        yForce = force / yForceDividend;

        pint = Utils.GetChildWithName(gameObject, "Pint");
        if(!pint) Debug.LogError("Pint not found");
    }

    protected void Update()
    {
        if(IsOnFire()){
            scoreMultiplier = 2;
            Invoke("FireCooldown", fireDuration);
            //TODO Add a shader to the ball
            //TODO Play a sound
        }
    }

    private void FireCooldown()
    {
        isOnFire = false;
        scoreMultiplier = 1;
    }

    public void ThrowPint(float forceFactor){
        if(pint.GetComponent<Pint>().canBeThrown){
            transform.DetachChildren();
            pint.GetComponent<Rigidbody>().isKinematic = false;
            pint.GetComponent<Pint>().canBeThrown = false;
            pint.GetComponent<Rigidbody>().useGravity = true;
            
            pint.GetComponent<Rigidbody>().AddRelativeForce(
                0,  // determined by my rotation
                yForce * forceFactor * 100,
                force * forceFactor * 100 
            );
        }
    }

    public void ResetThrow(){
        //force = 0;
    }

    public void Loose(){

    }

    public void Win(){

    }

    public bool IsOnFire(){
        if(comboValue <= 3)
            return false;

        comboValue = 0;
        return true;
    }

    public void IncreaseScore(int increase){
        score += increase;
    }

    public int GetScore(){
        return score;
    }

    public void SetComboValue(int val){
        comboValue = val;
    }

    public int GetComboValue(){
        return comboValue;
    }

     public int GetScoreMultiplier(){
        return scoreMultiplier;
    }
    
}
