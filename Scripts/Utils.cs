using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera cam, Vector3 position){
        position.z = cam.transform.position.z;
        return cam.ScreenToWorldPoint(position);
    }
}
