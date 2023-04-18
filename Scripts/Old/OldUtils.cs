using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldUtils : MonoBehaviour
{
    public static Vector2 ScreenToWorldCoord(Camera camera, Vector3 position){
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
