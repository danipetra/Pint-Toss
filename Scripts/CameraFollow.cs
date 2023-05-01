using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject camTarget;
    private Vector3 offset;
    private float zDistance = 10f;

    private void Awake() 
    {
       camTarget = GameObject.FindGameObjectWithTag("Camera Target"); 
    }
    
    private void Start()
    {
        transform.position = camTarget.transform.position;
        offset = transform.position - camTarget.transform.position;
    }

    private void Update()
    {
        transform.position =new Vector3(camTarget.transform.position.x + offset.x, camTarget.transform.position.y + offset.y, camTarget.transform.position.z - zDistance) ;
    }

    private void Transition(Vector3 destination, float time)
    {

    }
}
