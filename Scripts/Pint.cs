using UnityEngine;
public class Pint : MonoBehaviour
{
    public bool canBeThrown = true;
    private bool backboardThrow = false;


    private Vector3 startPosition;
    private Quaternion startRotation;
    private GameObject thrower;
    private Rigidbody rigidBody;
    private GameManager gameManager;
    private AudioManager audioManager;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        thrower = transform.parent.gameObject;
        rigidBody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnCollisionEnter(Collision other) {
        
        audioManager.Play("Bounce");
        
        if(other.gameObject.tag =="Floor"){
            thrower.GetComponent<Opponent>().SetComboValue(0);
            if(thrower.GetComponent<Opponent>().GetScoreMultiplier() > 1)
                thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() / 2);
            // Respawn me and my thrower
            gameManager.RespawnOpponent(thrower);
            if(backboardThrow){
                audioManager.Play("Boo");
                backboardThrow = false;
            }
        }

        if(other.gameObject.tag == "Backboard"){
            thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() * 2);
            audioManager.Play("Backboard");
            backboardThrow = true;
        }    
    }

     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag =="ScoreArea"){;
            audioManager.Play("Bounce");     
            audioManager.Play("Score");
            gameManager.AddScore(thrower, thrower.GetComponent<Opponent>().scoreText);
            thrower.GetComponent<Opponent>().SetComboValue(thrower.GetComponent<Opponent>().GetComboValue() + 1 );
            
            // Respawn me and my thrower
            gameManager.RespawnOpponent(thrower);
        }    
    }

    public void ResetRigitBody(){
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }
    
    public GameObject GetThrower(){
        return thrower;
    }

}
