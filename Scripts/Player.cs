using UnityEngine;
using System.Collections;
public class Player : Opponent{
    

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        base.Update();
    }

    public IEnumerator IncreaseForce(string s){
        while(true){
            Debug.Log(s);
            //Increase slider value

            //Increase force
            yield return null;
        }
    }

}
