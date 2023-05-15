using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class Opponent : MonoBehaviour
{
    // Scoring variables
    private int _score;
    private int _scoreMultiplier;

    // Variables to handle on fire mechanic
    [Range(3f,10f)] public float fireTotalDuration;
    private bool _fireCooldown;
    private Coroutine _fireCoroutine;

    public TMP_Text scoreText;
    private Slider _comboBar;
    //[SerializeField] TrajectorySimulator trajectorySimulator;
    
    // Throw variables 
    [SerializeField, Range(10, 25)]public float force; 
    [SerializeField, Range(0.5f, 2f)] public float maximumTime = 1.2f;
    [SerializeField, Range(2, 5)]private float _yForceDividend;
    private float _forceMultiplier = 100f;
    private float _yForce;
    private float _randomAngularVelocity = 10f;
    
    protected GameObject pint;

    protected void Awake()
    {
        _fireCooldown = false;
        _score = 0;
        scoreText.text = _score.ToString();
        _scoreMultiplier = 1;
        _yForce = force / _yForceDividend;

        pint = Utils.GetChildWithName(gameObject, "Pint");
        if(!pint) Debug.LogError("Pint not found");

        if(tag =="Player") _comboBar = GameObject.Find("Player Combo Bar").GetComponent<Slider>();
        else if(tag =="Enemy") _comboBar = GameObject.Find("Enemy Combo Bar").GetComponent<Slider>();
        if(!_comboBar)Debug.LogError(name+ ": " + "Combo Bar Not Found!");
    }

    protected void FixedUpdate()
    {
        if(_comboBar.value >= _comboBar.maxValue && !_fireCooldown)
        {
            _fireCooldown = true;
            _fireCoroutine = StartCoroutine(SetPintOnFire());
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
            pint.GetComponent<Rigidbody>().AddRelativeForce(0, force.y, force.z);

            // rotating the pint around itself at random velocity 
            pint.GetComponent<Rigidbody>().angularVelocity = new Vector3(
                UnityEngine.Random.Range(-_randomAngularVelocity, _randomAngularVelocity),
                UnityEngine.Random.Range(-_randomAngularVelocity, _randomAngularVelocity),
                0
            );
        }
    }

    public Vector3 CalculateForce(float forceFactor)
    {
        Vector3 force = new Vector3(
            0,
            _yForce * forceFactor * _forceMultiplier,
            this.force * forceFactor * _forceMultiplier 
        );
        return force;
    }

    public void PickPint()
    {
        pint.transform.parent = this.transform;

        FindObjectOfType<AudioManager>().Play("Drink");

        pint.transform.position  = transform.position;
        pint.transform.rotation = transform.rotation;
        pint.GetComponent<Pint>().ResetRigitBodyForces();        
        pint.GetComponent<Pint>().canBeThrown = true;
    }

    private IEnumerator SetPintOnFire()
    {
        float fireRemainingTime = fireTotalDuration;
        _scoreMultiplier *= 2;

        pint.GetComponent<Pint>().SetFireMode(true);

        // Calculating remaining time and updating the fireBar of the opponent
        while(fireRemainingTime >= 0)
        {
            fireRemainingTime -= Time.deltaTime;
            _comboBar.value = Utils.NormalizedDifference(fireRemainingTime, Time.deltaTime, _comboBar.maxValue, _comboBar.minValue);
            yield return new WaitForFixedUpdate();
        }
        
        EndPintOnFireMode();
        
        yield return null; 
    }

    // Ends the on fire mode of the Pint
    private void EndPintOnFireMode()
    {
        pint.GetComponent<Pint>().SetFireMode(false);
        if(_scoreMultiplier > 1 )
            _scoreMultiplier /= 2;
        _comboBar.value = 0;
        _fireCooldown = false;
        _fireCoroutine = null;
    }

    /*public void FireBreakdown(){
        StopCoroutine(fireCoroutine);
        if(scoreMultiplier > 1 )scoreMultiplier /= 2;
        comboBar.value = 0;
        fireCooldown = false;

        // FindObjectOfType<AudioManager>().Play("Fire Breakdown");
    }*/

    public void IncreaseScore(int increase) => _score += increase;

    public int GetScore() => _score;

    public void SetComboBarValue(int val) => _comboBar.value = val;

    public int GetComboValue() => (int)_comboBar.value;

    public int GetScoreMultiplier() => _scoreMultiplier;
    
    public void SetScoreMultiplier(int value) => _scoreMultiplier = value;
}
