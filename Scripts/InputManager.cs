using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] //run before any other scripts
public class InputManager : Singleton<InputManager>
{
    /*Observer Pattern
     Study it and also delegates and events
    */
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;

    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion
    private TouchControls touchControls;
    private Camera mainCamera;
    
    private void Awake() {
        touchControls = new TouchControls();
        mainCamera = Camera.main;
    }

    private void OnEnable() {
        touchControls.Enable();
    }

    private void OnDisable() {
        touchControls.Disable();
    }
    
    void Start()
    {
        /* 
            - Subscribe for the event when it starts (...started += ), 
            - Pass the context (ctx : information of the event),
            - Call the function 
        */
        touchControls.Touch.PrimaryContact.started += ctx => StartPrimaryTouch(ctx);
        /* 
            - Subscribe for the event when it ends (...canceled += ), 
            - Pass the context (ctx : information of the event),
            - Call the function 
        */
        touchControls.Touch.PrimaryContact.canceled += ctx => EndPrimaryTouch(ctx);
    }

    private void StartPrimaryTouch(InputAction.CallbackContext context){
        Debug.Log("Touch Start");
        if(OnStartTouch != null){
            OnStartTouch(
                Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), 
                (float)context.startTime
                );
        }
            
    }

     private void EndPrimaryTouch(InputAction.CallbackContext context){
        
        if(OnEndTouch != null){
            OnEndTouch(
                Utils.ScreenToWorld(mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>()), 
                (float)context.time
                );
        }

    }

    public Vector2 GetPrimaryPosition(){
        return Utils.ScreenToWorld (mainCamera, touchControls.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
