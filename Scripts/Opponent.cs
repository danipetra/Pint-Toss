using UnityEngine;
using TMPro;
using System;

public class Opponent : MonoBehaviour
{
    [Range(3f,5f)] public float fireDuration;
    // TODO make private
    public TMP_Text scoreText;
    //[SerializeField] TrajectorySimulator trajectorySimulator;
    [SerializeField, Range(10, 25)]public float force; //TODO change it to protected
    [SerializeField, Range(0.5f, 2f)] public float maximumTime = 1.2f;
    [SerializeField, Range(2, 5)]private float yForceDividend;
    [SerializeField]private int score;
    private Vector3 startPosition;
    //private Quaternion startRotation;
    private bool isOnFire;
    private int comboValue;
    private int scoreMultiplier;
    private float yForce;
    
    protected GameObject pint;

    protected void Start()
    {   
        startPosition = transform.position;
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
            scoreMultiplier *= 2;
            Invoke("FireCooldown", fireDuration);
            //TODO Add a shader to the ball
            //TODO Play a sound
        }
    }

    private void FireCooldown()
    {
        isOnFire = false;
        scoreMultiplier /= 2;
    }

    public void ThrowPint(float forceFactor){
        Debug.Log("Enemy throwing! :" + forceFactor);
        if(pint.GetComponent<Pint>().canBeThrown){
            transform.DetachChildren();
            pint.GetComponent<Pint>().canBeThrown = false;
            pint.GetComponent<Rigidbody>().isKinematic = false;
            pint.GetComponent<Rigidbody>().useGravity = true;
            
            Vector3 force = CalculateForce(forceFactor);
            //trajectorySimulator.SimulateTrajectory(pint, transform.position, forceFactor); // TODO Move it to do in realtime instead of only at the end
            pint.GetComponent<Rigidbody>().AddRelativeForce(
                0,  // determined by my rotation
                force.y,
                force.z 
            );
        }
    }

    public Vector3 CalculateForce(float forceFactor){
        Vector3 force = new Vector3(
            0,
            yForce * forceFactor * 100,
            this.force * forceFactor * 100 
        );
        return force;
    }

    

    public void Respawn(){
        pint.transform.parent = transform;
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
    
    public void SetScoreMultiplier(int value){
        scoreMultiplier = value;
    }
}
