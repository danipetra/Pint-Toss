using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera cam, Vector3 position){
        position.z = cam.transform.position.z;
        return cam.ScreenToWorldPoint(position);
    }

    public static float NormalizedDifference(float a, float b){
        float diff;
        diff = (a - b);
        return diff;
    }

    public static GameObject GetChildWithName(GameObject parent, string name){
        Transform childTransform = parent.transform.Find(name);
        if (childTransform != null) {
            return childTransform.gameObject;
        } else {
            return null;
        }
    }

    public static void LookAtLockedY(Transform sourceT, Transform targetT){
        Vector3 targetPosition = new Vector3 (targetT.position.x, sourceT.position.y ,targetT.position.z);
        sourceT.LookAt(targetPosition);
    }

}
