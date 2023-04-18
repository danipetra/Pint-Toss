using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    private InputManager swipeManager;
    public GameObject trail;

    [SerializeField]
    private float minimumDistance = .2f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField]
    private float directionThreshold = .9f;

    Vector2 startPosition;
    private float startTime;
    Vector2 endPosition;
    private float endTime;
    private Coroutine coroutine;

    private void Awake() {
        swipeManager = InputManager.Instance;
    }

    private void OnEnable() {
        swipeManager.OnStartTouch += SwipeStart;
        swipeManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable() {
        swipeManager.OnStartTouch -= SwipeStart;
        swipeManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time){
        startPosition = position;
        startTime = time;
        trail.SetActive(true);
        trail.transform.position = position;
        coroutine = StartCoroutine("Trail");
    }

    private IEnumerator Trail(){
        //Alternative way of using Update function, in this way the code is more clean
        while(true){
            trail.transform.position = swipeManager.GetPrimaryPosition();
            yield return null;// in this way you do not wait until next call
        }
    }

    private void SwipeEnd(Vector2 position, float time){
        trail.SetActive(false);
        endPosition = position;
        endTime = time;
        DetectSwipe();
        StopCoroutine(coroutine);
    }

    private void DetectSwipe(){
        if(Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime - startTime) <= maximumTime ){
                Debug.Log("Swipe Detected");
                Debug.DrawLine(startPosition, endPosition, Color.red, 5f);
                Vector3 direction = endPosition - startPosition;
                Vector2 direction2D = new Vector2(direction.x, direction.y).normalized; //It can be normalized cause for the direction I don't need a lenght
                SwipeDirection(direction2D);
            
            }
    }

    private void SwipeDirection(Vector2 direction){
        //Dot() is used to determine the difference in "direction" of two vectors
        if(Vector2.Dot(Vector2.up, direction) > directionThreshold){
            Debug.Log("Swipe Up");
        }
        if(Vector2.Dot(Vector2.down, direction) > directionThreshold){
            Debug.Log("Swipe Down");
        }
        if(Vector2.Dot(Vector2.right, direction) > directionThreshold){
            Debug.Log("Swipe Right");
        }
        if(Vector2.Dot(Vector2.left, direction) > directionThreshold){
            Debug.Log("Swipe Left");
        }

    }
}
