using UnityEngine;
public class Pint : MonoBehaviour
{
    public bool canBeThrown;
    public float throwerForce;
    [HideInInspector] public bool hitBackboard;
    [HideInInspector] public bool hasBackboardBlinkBonus;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private GameObject thrower;
    private Rigidbody rigidBody;

    private CollisionsManager _collisionsManager;

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
        if (!gameManager) Debug.LogError("game manager not found in the scene");

        _collisionsManager = FindObjectOfType<CollisionsManager>();
        if (!_collisionsManager) Debug.LogError("Collsions manager not found in the scene");
    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Floor"))
            _collisionsManager.HandleFloorCollision(this, thrower.GetComponent<Opponent>());
 
        if(other.gameObject.CompareTag("Backboard"))
            _collisionsManager.HandleBackboardCollision(this, thrower.GetComponent<Opponent>());
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Manage strike and score increase, depending on bonuses
        if(other.gameObject.CompareTag("ScoreArea"))
        {
            _collisionsManager.HandleBucketCollision(this, thrower.GetComponent<Opponent>());
            
            // Move this code
            int scoreIncrease = gameManager.UpdateScore(thrower, throwerForce, hasBackboardBlinkBonus);
              if(thrower.CompareTag("Player")){
                Debug.LogWarning(name + ": "+ " move the function to the opponent");
                thrower.GetComponent<Player>().ShowPoints(scoreIncrease); 
              }
        }    
    }

    public void ResetRigitBody()
    {
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    public void SetOnFire(bool fireMode){
        if(fireMode)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            audioManager.Play("Fire");
        } 
        else
        {
            GetComponentInChildren<ParticleSystem>().Stop();
            audioManager.Stop("Fire");
        }
    }

    public GameObject GetThrower() => thrower;

}
