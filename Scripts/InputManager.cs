using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

[DefaultExecutionOrder(-1)] // run before any other script
public class InputManager : MonoBehaviour
{
    /* Defines the delegates and the corrensponding events, to wich subscribe for listening for input */
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

    /* Swipe variables */
    [SerializeField, Range(0f, 1f)] private float _directionThreshold = 0.9f;
    [SerializeField, Range(15f, 50f)] private float _swipeSpeed = 25f; // Needed to avoid to update the slider too fast
    [SerializeField, Range(0f, 15f)] private float _minimumSwipeDistance = 1f;
    private Vector2 _startPosition, _endPosition;
    private float _startTime, _endTime;

    private PlayerControls _playerControls;
    private GameObject _player;
    private Slider _playerSlider;
    private Coroutine _coroutine;

    private void Awake() 
    {
        _playerControls = new PlayerControls();
        _player = GameObject.FindGameObjectWithTag( "Player" );
        if(!_player) Debug.LogError("Player not found");
        _playerSlider = GameObject.Find("Force Slider").GetComponent<Slider>();
        if(!_playerSlider) Debug.LogError("Force Slider GameObject not found");
    } 

    private void OnEnable()
    {
        _playerControls.Enable();
        /* Subscribe for the events */
        OnStartContact += SwipeStart;
        OnEndContact += SwipeEnd;
    }

    private void OnDisable()
    {
        _playerControls.Disable();
        /* Unsubscribe for the events */
        OnStartContact -= SwipeStart;
        OnEndContact -= SwipeEnd;
    }

    private void Start()
    {
        _playerControls.InGame.PrimaryContact.started += ctx => StartTouchPrimary(ctx);    
        _playerControls.InGame.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);   
    }


    /* When a contact starts or is canceled, return the position and the time it occurred (via CallbackContext variable) */ 
    private void StartTouchPrimary(InputAction.CallbackContext ctx) 
    { 
        if (OnStartContact != null) 
            OnStartContact(GetScreenPosition(), (float)ctx.startTime); 
        
    }

    private void EndTouchPrimary(InputAction.CallbackContext ctx) 
    { 
        if (OnEndContact != null) 
            OnEndContact(GetScreenPosition(), (float)ctx.time, _playerSlider.value); 
            
    }

    private void SwipeStart(Vector2 position, float time)
    {
        _coroutine = StartCoroutine(
            SwipeUpdate(_startTime)
            );
        _startPosition = position;
        _startTime = time;
    }


    public IEnumerator SwipeUpdate(float startTime)
    {
        float totalTime;
        Vector2 screenPos;
        float speed; 
        float yMaxMovement;
        yMaxMovement = Screen.height; // assuming that the max speed is obtained covering the entire screen in the given max time

        while(true)
        { 
            totalTime =  Time.time - startTime;
            screenPos = GetScreenPosition();
            speed = Utils.NormalizedDifference(screenPos.y , _startPosition.y, yMaxMovement, 0);
            speed *= (_player.GetComponent<Opponent>().maximumTime - (totalTime)) / _swipeSpeed; // the slider speed increase is proportional to the swipe time
            
            //Debug.Log("Swipe Time: " +  totalTime +" Y Movement: "+ speed);
            if( _playerSlider.value >= 1f || totalTime >= _player.GetComponent<Opponent>().maximumTime )
            {
                SwipeEnd(screenPos, Time.time, _playerSlider.value); // When called swipe end also stops this coroutine
            }
            else if(speed > 0)
            {   
                _playerSlider.value += speed;
            }
            yield return null;
        }

    }

    public void SwipeEnd(Vector2 position, float time, float sliderValue)
    {
        _endPosition = position;
        _endTime = time;
        float totalTime = _endTime - _startTime;
        DetectSwipeForce(sliderValue);
        StopCoroutine(_coroutine);
    }

    /* Called at the end of the Swipe */
    private void DetectSwipeForce(float forceFactor)
    {
        if(Vector3.Distance(_startPosition, _endPosition) >= _minimumSwipeDistance) 
            //&& (endTime - startTime) < maximumTime)
        {
            
            Vector3 direction = _endPosition + _startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            //SwipeDirection(direction2D);
            
            //float swipeIntensity = //Utils.NormalizedDifference(endPosition.y, startPosition.y);
            _player.GetComponent<Opponent>().ThrowPint(forceFactor);
        }
    }

    /* Calls an event based on the swipe direction */
    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > _directionThreshold)
        {
            if (OnSwipeUp != null) OnSwipeUp();
        }
        else if (Vector2.Dot(Vector2.down, direction) > _directionThreshold)
        {
            if (OnSwipeDown != null) OnSwipeDown();
        }

    }

    public Vector2 GetScreenPosition() => _playerControls.InGame.PrimaryPosition.ReadValue<Vector2>();
}
