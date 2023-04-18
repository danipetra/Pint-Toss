using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SwipeData
{
    //Time variables
    public float startTime;      
    public float endTime;
    public float totalTime;

    //Cooridinate Variables
    public float maxY;
    public float startY;
    public float distance;
    public float normalizedDistance; //normFactor = sqrt((x1-x2)^2 + (x1-x2)^2);

    //Measures Variables
    public float speed;
    public float startingSpeed;
    public float finalSpeed;
    public float acceleration;
}
