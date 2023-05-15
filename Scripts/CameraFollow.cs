using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject _camTarget;
    private Vector3 _offset;
    private float _zDistance = 10f;

    private void Awake() 
    {
       _camTarget = GameObject.FindGameObjectWithTag("Camera Target"); 
    }
    
    private void Start()
    {
        transform.position = _camTarget.transform.position;
        _offset = transform.position - _camTarget.transform.position;
    }

    private void Update()
    {
        transform.position =new Vector3(_camTarget.transform.position.x + _offset.x, _camTarget.transform.position.y + _offset.y, _camTarget.transform.position.z - _zDistance) ;
    }

    private void Transition(Vector3 destination, float time)
    {

    }
}
