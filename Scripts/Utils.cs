using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera cam, Vector3 position){
        position.z = cam.transform.position.z;//cam.nearClipPlane;
        //position.y = -position.y;
        return cam.ScreenToWorldPoint(position);
    }

    private static float NormalizedDifference(float a, float b){
        float diff;
        diff = (a - b) / Screen.height;
        return diff;
    }

}
