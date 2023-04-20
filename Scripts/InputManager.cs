using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] // run before any other script
public class InputManager : MonoBehaviour
{
    /* Defines the events and the corrensponding functions called when they occur */
    #region Events
    private delegate void StartContact(Vector2 position, float time);
    private event StartContact OnStartContact;
    private delegate void EndContact(Vector2 position, float time);
    private event EndContact OnEndContact;

    public delegate void UpSwipe();
    public event UpSwipe OnSwipeUp;
    public delegate void DownSwipe();
    public event DownSwipe OnSwipeDown;
    #endregion

    /* Swipe variables: put into a class */
    [SerializeField, Range(0f, 30f)] private float minimumDistance = 15f;
    [SerializeField, Range(0.5f, 1.2f)] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;

    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    private PlayerControls playerControls;

    private void Awake() {
        playerControls = new PlayerControls();
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
            OnEndContact(GetScreenPosition(), (float)ctx.time); 
            
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    /* Called at the end of the Swipe */
    private void DetectSwipe()
    {
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) < maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
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
