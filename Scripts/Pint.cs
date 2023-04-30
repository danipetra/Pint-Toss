using UnityEngine;
public class Pint : MonoBehaviour
{
    public bool canBeThrown;
    public float throwerForce;
    private bool hitBackboard;
    private bool hasBackboardBlinkBonus;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private GameObject thrower;
    private Rigidbody rigidBody;
    private GameManager gameManager;
    private AudioManager audioManager;


    private void Awake()
    {
        canBeThrown = true;
        hitBackboard = false;
        hasBackboardBlinkBonus = false;

        startPosition = transform.position;
        startRotation = transform.rotation;
        thrower = transform.parent.gameObject;
        rigidBody = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    /* TODO manage the events on a bucket script, or even better with an Observer Design Pattern */
    private void OnCollisionEnter(Collision other) 
    {
        audioManager.Play("Bounce");

        // Manage floor hit
        if(other.gameObject.tag =="Floor")
        {
            thrower.GetComponent<Opponent>().SetComboBarValue(0);

            if(thrower.GetComponent<Opponent>().GetScoreMultiplier() > 1)
                    thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() / 2);
                   
            // Respawn me and my thrower
            gameManager.RespawnOpponent(thrower);
            
            if(hitBackboard)
            {
                audioManager.Play("Boo");
                hitBackboard = false;
                hasBackboardBlinkBonus = false;
                if(thrower.GetComponent<Opponent>().GetScoreMultiplier() > 1)
                    thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() / 2);
                
            }
        }

        // Manage backboard hit 
        if(other.gameObject.tag == "Backboard")
        {
            thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() * 2);
            audioManager.Play("Backboard");
            hitBackboard = true;

            if(other.gameObject.GetComponent<Backboard>().IsBlinking())
            {
                hasBackboardBlinkBonus = true;
            }
        }    
    }

     private void OnTriggerEnter(Collider other) 
     {
        // Manage strike and score increase, depending on bonuses
        if(other.gameObject.tag =="ScoreArea")
        {
            audioManager.Play("Bounce");     
            audioManager.Play("Score");

            int scoreIncrease = gameManager.UpdateScore(thrower, throwerForce, hasBackboardBlinkBonus);
            if(hasBackboardBlinkBonus) // TODO Deactivate blink on backboard
            Debug.Log("COMBO! " + thrower.GetComponent<Opponent>().GetComboValue() + 1 );
            thrower.GetComponent<Opponent>().SetComboBarValue(thrower.GetComponent<Opponent>().GetComboValue() + 1 );
            if(hitBackboard && thrower.GetComponent<Opponent>().GetScoreMultiplier() > 1)
                    thrower.GetComponent<Opponent>().SetScoreMultiplier(thrower.GetComponent<Opponent>().GetScoreMultiplier() / 2);
            
            if(thrower.tag == "Player"){
                thrower.GetComponent<Player>().ShowPoints(scoreIncrease);
            }

            // Respawn me and my thrower
            gameManager.RespawnOpponent(thrower);
        }    
    }

    public void ResetRigitBody()
    {
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public GameObject GetThrower() => thrower;

}
