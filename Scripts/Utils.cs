using UnityEngine;
public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Vector2 position){
        return Camera.main.ScreenToWorldPoint(position);
    }

    public static float NormalizedDifference(float a, float b, float max, float min){
        float diff;
        diff = (a - b)/ (max - min);
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
        Vector3 direction = -(sourceT.position - targetT.position);
        // Set the y-rotation of the source object to the angle between the direction vector and the target object's forward vector
        sourceT.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // Set the z-rotation of the source object to zero to keep it level
        sourceT.rotation = Quaternion.Euler(sourceT.rotation.eulerAngles.x, sourceT.rotation.eulerAngles.y, 0);
    }

    private Vector3 RandomPointOnCircleEdge(float radius, int fixedAxisIndex)
    {
        var point = Random.insideUnitCircle.normalized * radius;
        return new Vector3(point.x, 0, point.y);
    }

    /* Function that picks a random value using a Gaussian normal distribution */
    public static float RandomGaussian(float minValue, float maxValue)
    {
        float u, v, S;
        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);
 
        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
 
        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

    public static Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angle) {
         return angle * ( point - pivot) + pivot;
     }

}
