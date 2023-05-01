using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class Opponent : MonoBehaviour
{
    // Variables to handle opponent scoring and fire mechanic
    private int score;
    private int scoreMultiplier;
    [Range(3f,10f)] public float fireTotalDuration;
    private bool fireCooldown;
    private Coroutine fireCoroutine;

    public TMP_Text scoreText;
    private Slider comboBar;
    //[SerializeField] TrajectorySimulator trajectorySimulator;
    
    // Throw variables that do not depend on player input
    [SerializeField, Range(10, 25)]public float force; 
    [SerializeField, Range(0.5f, 2f)] public float maximumTime = 1.2f;
    [SerializeField, Range(2, 5)]private float yForceDividend;
    private float yForce;
    
    protected GameObject pint;

    protected void Awake()
    {
        fireCooldown = false;
        score = 0;
        scoreText.text = score.ToString();
        scoreMultiplier = 1;
        yForce = force / yForceDividend;

        pint = Utils.GetChildWithName(gameObject, "Pint");
        if(!pint) Debug.LogError("Pint not found");

        if(tag =="Player") comboBar = GameObject.Find("Player Combo Bar").GetComponent<Slider>();
        else if(tag =="Enemy") comboBar = GameObject.Find("Enemy Combo Bar").GetComponent<Slider>();
        if(!comboBar)Debug.LogError("Combo Bar Not Found!");
    }

    protected void FixedUpdate()
    {
        if(comboBar.value >= comboBar.maxValue && !fireCooldown)
        {
            fireCooldown = true;
            fireCoroutine = StartCoroutine(Fire());
        }
    }

    public void ThrowPint(float forceFactor)
    {
        // Debug.Log(gameObject.tag +" throw with force :" + forceFactor);
        if(pint.GetComponent<Pint>().canBeThrown)
        {
            transform.DetachChildren();
            pint.GetComponent<Pint>().canBeThrown = false;
            pint.GetComponent<Pint>().throwerForce = forceFactor;
            pint.GetComponent<Rigidbody>().isKinematic = false;
            pint.GetComponent<Rigidbody>().useGravity = true;
            
            //trajectorySimulator.SimulateTrajectory(pint, transform.position, forceFactor);

            // Calculate the corrensponding force and apply it to the pint
            Vector3 force = CalculateForce(forceFactor);
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

    private IEnumerator Fire()
    {
        float fireRemainingTime = fireTotalDuration;
        scoreMultiplier *= 2;
        pint.GetComponent<Pint>().SetOnFire(true);

        while(fireRemainingTime >= 0)
        {
            fireRemainingTime -= Time.deltaTime;
            comboBar.value = Utils.NormalizedDifference(fireRemainingTime, Time.deltaTime, comboBar.maxValue, comboBar.minValue);
            yield return new WaitForFixedUpdate();
        }
        
        // Returning to the state before going on fire
        pint.GetComponent<Pint>().SetOnFire(false);
        if(scoreMultiplier > 1 )
            scoreMultiplier /= 2;
        comboBar.value = 0;
        fireCooldown = false;
        fireCoroutine = null;
        yield return null;
        
    }

    /*public void FireBreakdown(){
        StopCoroutine(fireCoroutine);
        if(scoreMultiplier > 1 )scoreMultiplier /= 2;
        comboBar.value = 0;
        fireCooldown = false;

        // FindObjectOfType<AudioManager>().Play("Fire Breakdown");
    }*/

    public void IncreaseScore(int increase) => score += increase;

    public int GetScore() => score;

    public void SetComboBarValue(int val) => comboBar.value = val;

    public int GetComboValue() => (int)comboBar.value;

    public int GetScoreMultiplier() => scoreMultiplier;
    
    public void SetScoreMultiplier(int value) => scoreMultiplier = value;
}
