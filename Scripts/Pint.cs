using UnityEngine;
public class Pint : MonoBehaviour
{
    public bool canBeThrown = true;


    private Vector3 startPosition;
    private Quaternion startRotation;
    private GameObject thrower;
    private Rigidbody rigidBody;
    private GameManager gameManager;

    private void Awake()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
        thrower = transform.parent.gameObject;
        rigidBody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other) {
        
        FindObjectOfType<AudioManager>().Play("Floor Touch");
        
        if(other.gameObject.tag =="Floor"){
            thrower.GetComponent<Opponent>().SetComboValue(0);
            if(thrower.GetComponent<Opponent>().GetScoreMultiplier() > 1)
                thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() / 2);
            // Respawn me and my thrower
            Reset();
            gameManager.RespawnOpponent(thrower);
        }

        if(other.gameObject.tag == "Backboard"){
            thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() * 2);
        }    
    }

     private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag =="ScoreArea"){;
            FindObjectOfType<AudioManager>().Play("Floor Touch");     
            FindObjectOfType<AudioManager>().Play("Score");
            gameManager.AddScore(thrower, thrower.GetComponent<Opponent>().scoreText);
            thrower.GetComponent<Opponent>().SetComboValue(thrower.GetComponent<Opponent>().GetComboValue() + 1 );
            
            // Respawn me and my thrower
            Reset();
            gameManager.RespawnOpponent(thrower);
        }    
    }

    public void Reset(){
        transform.position = startPosition/*new Vector3(0,0,0)*/;
        transform.rotation = startRotation/*new Quaternion(0,0,0,0)*/;
        rigidBody.useGravity = false;
        /* Removing previous forces applied while throwing*/
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero; 
        canBeThrown = true;
    }

    
    public Rigidbody GetRigidBody(){
        return rigidBody;
    } 

    public GameObject GetThrower(){
        return thrower;
    }

}
