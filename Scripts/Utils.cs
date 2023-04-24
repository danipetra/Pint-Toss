using UnityEngine;
public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera cam, Vector3 position){
        position.z = cam.transform.position.z;
        return cam.ScreenToWorldPoint(position);
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
        Vector3 lookPosition = new Vector3 (targetT.position.x, sourceT.position.y ,targetT.position.z);
        sourceT.LookAt(lookPosition);
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

}
