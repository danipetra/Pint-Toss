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
    private GameManager gameManager;

    private void Awake()
    {
        startPosition = transform.position;
        thrower = transform.parent.gameObject;

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
            thrower.GetComponent<Opponent>().consecutiveShots = 0;
        }    
    }

     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name =="Bucket"){
            ResetPosition();
            Debug.LogWarning("You Scored!");
            //Addpointstoplayer!();

            FindObjectOfType<AudioManager>().Play("Score");
            FindObjectOfType<AudioManager>().Play("Floor Touch");

            gameManager.AddScore(thrower, thrower.GetComponent<Opponent>().scoreText);

            thrower.GetComponent<Opponent>().consecutiveShots++;
        }    
    }

    public void ResetPosition(){
        transform.position = startPosition;
        rigidBody.useGravity = false;
        canBeThrown = true;
        //Removing previous forces
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero; 
    }
}
