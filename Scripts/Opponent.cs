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

    public void ThrowPint(float forceFactor)
    {
        Debug.Log(gameObject.tag +" throw with force :" + forceFactor);
        if(pint.GetComponent<Pint>().canBeThrown){
            transform.DetachChildren();
            pint.GetComponent<Pint>().canBeThrown = false;
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

            // Add a rotation to the pint around itself
            Vector3 rotationAxis = new Vector3(UnityEngine.Random.Range(0f, 1f), 0, UnityEngine.Random.Range(-1f, 1f)).normalized;
            float rotationSpeed = UnityEngine.Random.Range(10f, 20f);
            pint.transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);

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

    public void SetComboValue(int val) => comboValue = val;

    public int GetComboValue() => comboValue;

    public int GetScoreMultiplier() => scoreMultiplier;
    
    public void SetScoreMultiplier(int value) => scoreMultiplier = value;
}
