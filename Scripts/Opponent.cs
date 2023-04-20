using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Opponent : MonoBehaviour
{
    [SerializeField]private float yThrowForce = 1f;
    [SerializeField]private float zThrowForce = 15f;
    [Range(3f,5f)] public float fireDuration;

    public TMP_Text scoreText;
    public int score;
    private Rigidbody pintRigidBody;

    public bool isOnFire;
    public int consecutiveShots;
    public int scoreMultiplier;
    private Pint pintScript;
    
    protected void Start()
    {   
        //InitVariables()
        scoreText.text = score.ToString();
        scoreMultiplier = 1;
        isOnFire = false;
        consecutiveShots = 0;

        pintRigidBody = GetComponentInChildren<Rigidbody>();
        if(!pintRigidBody){
            Debug.LogError("Pint RigidBody Not found");
        }

        pintScript = GetComponentInChildren<Pint>();
        if(!pintScript){
            Debug.LogError("Pint Script Not found");
        }
    }

    // Update is called once per frame
    protected void Update()
    {
        if(IsOnFire()){
            scoreMultiplier = 2;
            Invoke("FireCooldown", fireDuration);
        }
    }

    private void FireCooldown()
    {
        isOnFire = false;
        scoreMultiplier = 1;
    }

    public void ThrowPint(GameObject Pint, Vector2 direction, float force){
        if(pintScript.canBeThrown){
            pintRigidBody.isKinematic = false;
            pintRigidBody.AddForce(-direction.x * yThrowForce,
                               -direction.y * yThrowForce,
                               zThrowForce / force);
            //pintRigidBody.AddForce(YThrowForce, YThrowForce, ZThrowForce / touchTotalTime);
            pintScript.GetComponent<Pint>().canBeThrown = false;
            pintRigidBody.useGravity = true;
        }
    }

    public bool IsOnFire(){
        if(consecutiveShots <= 3)
            return false;

        consecutiveShots = 0;
        return true;
    }

    void Loose(){

    }

    void Win(){

    }


}
