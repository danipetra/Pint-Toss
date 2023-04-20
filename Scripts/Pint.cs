using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pint : MonoBehaviour
{
    public bool canBeThrown = true;

    private GameObject thrower;
    private Rigidbody rigidBody;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private GameManager gameManager;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        rigidBody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    public Rigidbody getRigidBody(){
        return rigidBody;
    } 

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag =="Floor"){
            ResetPosition();
            FindObjectOfType<AudioManager>().Play("Floor Touch");
            thrower.GetComponent<Opponent>().SetComboValue(0);
        }    
    }

     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name =="Bucket"){
            Debug.LogWarning("You Scored!");

            FindObjectOfType<AudioManager>().Play("Score");
            FindObjectOfType<AudioManager>().Play("Floor Touch");

            gameManager.AddScore(thrower, thrower.GetComponent<Opponent>().scoreText);
            thrower.GetComponent<Opponent>().SetComboValue(thrower.GetComponent<Opponent>().GetComboValue() +1 );

            ResetPosition();
        }    
    }

    public void ResetPosition(){
        transform.position = startPosition;
        transform.rotation = startRotation;
        rigidBody.useGravity = false;
        canBeThrown = true;
        //Removing previous forces
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero; 
    }
}
