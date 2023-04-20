using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OldInputManager : MonoBehaviour
{
    

    [Range(.3f,2f)]
    public float touchMaxTime = 2f;
    public GameObject playerPint;
    public Player playerScript;
    public Slider forceSlider;
    private Rigidbody playerPintRigidBody;
    private bool playerIsTouching = false;//Remove this variable later
    private Vector2 startPosition, endPosition;
    private Vector2 direction;
    
    // Start is called before the first frame update
    private void Awake()
    {
        forceSlider.maxValue = touchMaxTime;

        Input.simulateMouseWithTouches = true;
        playerPintRigidBody = playerPint.GetComponent<Rigidbody>();
        if(!playerPintRigidBody)
            Debug.LogError("PlayerPintRigidBody NOT FOUND");
    }

    // Update is called once per frame
    void Update()
    {
        ManagePlayerSwipe();
    }

    void ManagePlayerSwipe(){
        CheckPlayerTouch();
        //CheckPlayerClick(swipeData);
    }

    private void CheckPlayerTouch(){
        if(Input.touchCount > 0){
            //Get touch and initialize its data
            Touch touch = Input.GetTouch(0);
            SwipeData swipeData = new SwipeData();
            
            if(touch.phase == UnityEngine.TouchPhase.Began /*&& !playerIsTouching*/){
                StartSwipe(touch.position.y, swipeData);  
                
                /*var output = JsonUtility.ToJson(swipeData, true);
                Debug.Log("START" + output);*/           
            }

            if(touch.phase == UnityEngine.TouchPhase.Moved /*&& Time.time - swipeData.startTime < touchMaxTime&& swipeData.maxY < touch.position.y && playerIsTouching */){ 
                MoveSwipe(swipeData);
                forceSlider.value = swipeData.normalizedDistance; 
                
                var output = JsonUtility.ToJson(swipeData, true);
                Debug.Log("DRAG" + output);     
            }

            if(/*playerIsTouching && Time.time - swipeData.startTime >= touchMaxTime ||*/ touch.phase == UnityEngine.TouchPhase.Ended){
           
                if(swipeData.maxY < touch.position.y)
                    swipeData.maxY = touch.position.y;
                EndSwipe(swipeData);
            
                var output = JsonUtility.ToJson(swipeData, true);
                Debug.Log("END" + output);

                playerScript.ThrowPint(direction, forceSlider.value);
        }
        
        }     
      
    }

    
    /*private void CheckPlayerClick(){
        
        if(Input.GetMouseButtonDown(0)){
            CalculateStartTouchData();
        }

        if(Input.GetMouseButtonUp(0) || 
            playerIsTouching && Time.time - touchTimeStart >= touchMaxTime){
            
            CalculateThrowForces(Input.mousePosition);

            PlayerScript.ThrowPint(PlayerPint,direction,touchTotalTime);
        }

    }*/

    private void StartSwipe(float startY, SwipeData swipeData){     
            playerIsTouching = true;

            swipeData.startY = startY;
            swipeData.maxY = swipeData.startY;
            swipeData.startTime = Time.time;
            //!TOREMOVE
            //startPosition = Input.mousePosition;
    }

    private void MoveSwipe(SwipeData swipeData)
    {
        swipeData.distance = swipeData.maxY - swipeData.startY;
        swipeData.normalizedDistance = (swipeData.distance) / Screen.height;
    }


    private void EndSwipe(SwipeData swipeData){
            playerIsTouching = false;
            
            swipeData.distance = swipeData.maxY - swipeData.startY;
            swipeData.normalizedDistance = (swipeData.distance) / Screen.height;

            swipeData.endTime = Time.time;
            
            swipeData.totalTime = swipeData.endTime - swipeData.startTime;

            if(swipeData.totalTime > touchMaxTime)
                swipeData.totalTime = touchMaxTime;

            direction = startPosition - endPosition;
    }

}
