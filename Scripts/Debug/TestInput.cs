using System.Collections;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    
    public GameObject trail;
    
    private InputManager _inputManager;
    private Camera _mainCamera;

    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _mainCamera = Camera.main;
    }

    private void Update() {
        trail.transform.position = _inputManager.GetScreenPosition();
    }

    private void OnEnable()
    {
        _inputManager.OnSwipeUp += OnSwipeUp;
        _inputManager.OnSwipeDown += OnSwipeDown;
    }

    private void OnDisable()
    {
        _inputManager.OnSwipeUp -= OnSwipeUp;
        _inputManager.OnSwipeDown -= OnSwipeDown;
    }

    private void OnSwipeUp() => Debug.Log("Up swipe detected");
    private void OnSwipeDown() => Debug.Log("Down swipe detected");

    public IEnumerator TraceTrail(){
        while(true){
            trail.transform.position = _inputManager.GetScreenPosition();
            yield return null;// in this way you do not wait until next call
        }
    }

    private IEnumerator EnemyPerfectThrowDebugging(Enemy enemy){
        Debug.LogWarning("Enemy's cheatin!");
        float force = Mathf.Abs( Utils.RandomGaussian(.40f, .44f) );
        float delay = force + Mathf.Abs( Utils.RandomGaussian(0, force) );

        yield return new WaitForSeconds(delay);
        enemy.ThrowPint(force);
    }
    
}