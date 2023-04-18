using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private GameObject player;
    private Vector3 offset;

    private void Awake() {
       player = GameObject.FindGameObjectWithTag("Camera Target"); 
    }
    
    void Start()
    {
        transform.position = player.transform.position;
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position =new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, player.transform.position.z -10f) ;
    }
}
