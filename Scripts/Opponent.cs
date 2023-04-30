using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Opponent : MonoBehaviour
{
    [Range(3f,5f)] public float fireDuration;
    // TODO make private
    public TMP_Text scoreText;
    private Slider comboBar;
    //[SerializeField] TrajectorySimulator trajectorySimulator;
    [SerializeField, Range(10, 25)]public float force; //TODO change it to protected
    [SerializeField, Range(0.5f, 2f)] public float maximumTime = 1.2f;
    [SerializeField, Range(2, 5)]private float yForceDividend;
    private int score;
    private bool isOnFire;
    private int comboValue;
    private int scoreMultiplier;
    private float yForce;
    
    protected GameObject pint;

    protected void Awake()
    {
        score = 0;
        scoreText.text = score.ToString();
        scoreMultiplier = 1;
        isOnFire = false;
        comboValue = 0;
        yForce = force / yForceDividend;

        pint = Utils.GetChildWithName(gameObject, "Pint");
        if(!pint) Debug.LogError("Pint not found");

        if(tag =="Player") comboBar = GameObject.Find("Player Combo Bar").GetComponent<Slider>();
        else if(tag =="Enemy") comboBar = GameObject.Find("Enemy Combo Bar").GetComponent<Slider>();
        if(!comboBar)Debug.LogError("Combo Bar Not Found!");

    }

    protected void Update()
    {
        
        if(comboBar.value >= comboBar.maxValue)
        {
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

    public void ThrowPint(float forceFactor)
    {
        Debug.Log(gameObject.tag +" throw with force :" + forceFactor);
        if(pint.GetComponent<Pint>().canBeThrown){
            transform.DetachChildren();
            pint.GetComponent<Pint>().canBeThrown = false;
            pint.GetComponent<Pint>().throwerForce = forceFactor;
            pint.GetComponent<Rigidbody>().isKinematic = false;
            pint.GetComponent<Rigidbody>().useGravity = true;
            
            // Calculate the corrensponding force and apply it to the pint
            Vector3 force = CalculateForce(forceFactor);
            //trajectorySimulator.SimulateTrajectory(pint, transform.position, forceFactor);
            pint.GetComponent<Rigidbody>().AddRelativeForce(
                0,  // determined by my rotation
                force.y,
                force.z 
            );

            pint.GetComponent<Rigidbody>().angularVelocity = new Vector3(
                UnityEngine.Random.Range(1f, 10f),
                UnityEngine.Random.Range(-5f, 5f),
                0
            );
        }
    }

    public Vector3 CalculateForce(float forceFactor)
    {
        Vector3 force = new Vector3(
            0,
            yForce * forceFactor * 100,
            this.force * forceFactor * 100 
        );
        return force;
    }

    public void PickPint()
    {
        pint.transform.parent = this.transform;
        FindObjectOfType<AudioManager>().Play("Drink");

        pint.transform.position  = transform.position;
        pint.transform.rotation = transform.rotation;
        pint.GetComponent<Pint>().ResetRigitBody();        
        pint.GetComponent<Pint>().canBeThrown = true;
    }

    public bool IsOnFire()
    {
        if(comboValue <= 3)
            return false;

        comboValue = 0;
        return true;
    }

    public void IncreaseScore(int increase) => score += increase;

    public int GetScore() => score;

    public void SetComboBarValue(int val) => comboBar.value = val;

    public int GetComboValue() => (int)comboBar.value;

    public int GetScoreMultiplier() => scoreMultiplier;
    
    public void SetScoreMultiplier(int value) => scoreMultiplier = value;
}
