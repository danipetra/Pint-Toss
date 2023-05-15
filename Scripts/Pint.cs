using UnityEngine;
public class Pint : MonoBehaviour
{
    public bool canBeThrown;
    public float throwerForce;
    [HideInInspector] public bool hitBackboard;
    [HideInInspector] public bool hasBackboardBlinkBonus;

    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private GameObject _thrower;
    private Rigidbody _myRigidBody;

    private CollisionsManager _collisionsManager;
    // Remove this two 
    private GameManager _gameManager;
    private AudioManager _audioManager;

    private void Awake()
    {
        canBeThrown = true;
        hitBackboard = false;
        hasBackboardBlinkBonus = false;

        _startPosition = transform.position;
        _startRotation = transform.rotation;
        _thrower = transform.parent.gameObject;
        _myRigidBody = GetComponent<Rigidbody>();

        _gameManager = FindObjectOfType<GameManager>();
        if (!_gameManager) Debug.LogError("game manager not found in the scene");

        _collisionsManager = FindObjectOfType<CollisionsManager>();
        if (!_collisionsManager) Debug.LogError("Collisions manager not found in the scene");
    }

    private void FixedUpdate() 
    {

    }


    private void OnCollisionEnter(Collision other) 
    {
        if(other.gameObject.CompareTag("Floor"))
            _collisionsManager.HandleFloorCollision(this, _thrower.GetComponent<Opponent>());
 
        if(other.gameObject.CompareTag("Backboard"))
            _collisionsManager.HandleBackboardCollision(this, _thrower.GetComponent<Opponent>());
    }

    private void OnTriggerEnter(Collider other) 
    {
        // Manage strike and score increase, depending on bonuses
        if(other.gameObject.CompareTag("ScoreArea"))
        {
            _collisionsManager.HandleBucketCollision(this, _thrower.GetComponent<Opponent>());
            
            //Increases score depending on bonuses and updates the UI elements
            _gameManager.HandleScoreIncrease(_thrower.GetComponent<Opponent>(), throwerForce, hasBackboardBlinkBonus);
        }    
    }

    public void ResetRigitBodyForces()
    {
        _myRigidBody.useGravity = false;
        _myRigidBody.velocity = Vector3.zero;
        _myRigidBody.angularVelocity = Vector3.zero;
    }

    public void SetFireMode(bool fireMode){
        if(fireMode)
        {
            GetComponentInChildren<ParticleSystem>().Play();
            _audioManager.Play("Fire");
        } 
        else
        {
            GetComponentInChildren<ParticleSystem>().Stop();
            _audioManager.Stop("Fire");
        }
    }

    public GameObject GetThrower() => _thrower;

}
