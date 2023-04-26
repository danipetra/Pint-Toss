using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(-1)] // run before any other script
public class InputManager : MonoBehaviour
{
    /* Defines the events and the corrensponding functions called when they occur */
    #region Events
    private delegate void StartContact(Vector2 position, float time);
    private event StartContact OnStartContact;
    private delegate void EndContact(Vector2 position, float time, float sliderValue);
    private event EndContact OnEndContact;

    public delegate void UpSwipe();
    public event UpSwipe OnSwipeUp;
    public delegate void DownSwipe();
    public event DownSwipe OnSwipeDown;
    #endregion

    /* Swipe variables: put into a class */
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;
    [SerializeField, Range(15f, 50f)] private float _swipeSpeed = 25f; // Needed to avoid to update the slider to0 fast
    [SerializeField, Range(0f, 15f)] private float minimumSwipeDistance = 1f;
    
    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    private PlayerControls playerControls;
    private GameObject player;
    private Slider playerSlider;
    private Coroutine coroutine;

    private void Awake() {
        playerControls = new PlayerControls();
        player = GameObject.FindGameObjectWithTag( "Player" );
        if(!player) Debug.LogError("Player not found");
        playerSlider = GameObject.Find("Force Slider").GetComponent<Slider>();
        if(!playerSlider) Debug.LogError("Force Slider GameObject not found");
    } 

    private void OnEnable()
    {
        playerControls.Enable();
        /* Subscribe for the events */
        OnStartContact += SwipeStart;
        OnEndContact += SwipeEnd;
    }

    private void OnDisable()
    {
        playerControls.Disable();
        /* Unsubscribe for the events */
        OnStartContact -= SwipeStart;
        OnEndContact -= SwipeEnd;
    }

    private void Start()
    {
        playerControls.InGame.PrimaryContact.started += ctx => StartTouchPrimary(ctx);    
        playerControls.InGame.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);   
    }


    /* When a contact starts or is canceled, return the position and the time it occurred (via CallbackContext variable) */ 
    private void StartTouchPrimary(InputAction.CallbackContext ctx) { 
        if (OnStartContact != null) 
            OnStartContact(GetScreenPosition(), (float)ctx.startTime); 
        
    }
    private void EndTouchPrimary(InputAction.CallbackContext ctx) { 
        if (OnEndContact != null) 
            OnEndContact(GetScreenPosition(), (float)ctx.time, playerSlider.value); 
            
    }

    private void SwipeStart(Vector2 position, float time)
    {
        coroutine = StartCoroutine(
            SwipeUpdate(startTime)
            );
        startPosition = position;
        startTime = time;
    }

    public IEnumerator SwipeUpdate(float startTime){
        float totalTime;
        Vector2 screenPos;
        float speed; 
        float yMaxMovement;
        
        yMaxMovement = Screen.height; // assuming that the max speed is obtained covering the entire screen in the given max time

        while(true){ 
            totalTime =  Time.time - startTime;
            screenPos = GetScreenPosition();
            speed = Utils.NormalizedDifference(screenPos.y , startPosition.y, yMaxMovement, 0);
            speed *= (player.GetComponent<Opponent>().maximumTime - (totalTime)) / _swipeSpeed; // the slider speed increase is proportional to the swipe time
            Debug.Log("Swipe Time: " +  totalTime +" Y Movement: "+ speed);
            if( playerSlider.value >= 1f || totalTime >= player.GetComponent<Opponent>().maximumTime ){
                SwipeEnd(screenPos, Time.time, playerSlider.value); // When called swipe end also stops this coroutine
            }
            else{   
                playerSlider.value += speed;
            }
            yield return null;
        }

    }

    public void SwipeEnd(Vector2 position, float time, float sliderValue)
    {
        endPosition = position;
        endTime = time;
        float totalTime = endTime - startTime;
        DetectSwipeForce(sliderValue);
        StopCoroutine(coroutine);
    }

    /* Called at the end of the Swipe */
    private void DetectSwipeForce(float forceFactor)
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumSwipeDistance) 
            //&& (endTime - startTime) < maximumTime)
        {
            
            Vector3 direction = endPosition + startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            //SwipeDirection(direction2D);
            
            //float swipeIntensity = //Utils.NormalizedDifference(endPosition.y, startPosition.y);
            Debug.LogWarning("Swipe intensity: " + forceFactor);
            player.GetComponent<Opponent>().ThrowPint(forceFactor);
        }
    }

    /* Calls an event based on the swipe direction */
    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            if (OnSwipeUp != null) OnSwipeUp();
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            if (OnSwipeDown != null) OnSwipeDown();
        }

    }

    public Vector2 GetScreenPosition() { return playerControls.InGame.PrimaryPosition.ReadValue<Vector2>(); }
}
