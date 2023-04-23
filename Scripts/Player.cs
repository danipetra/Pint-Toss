using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : Opponent{

    private GameObject forceSliderO;
    
    new void Start()
    {
        base.Start();

        forceSliderO = (GameObject)GameObject.Find("Force Slider");
        if(!forceSliderO) Debug.LogError("Force Slider GameObject not found");
    }

    new void Update()
    {
        base.Update();
    }

    public IEnumerator IncreaseForce(string s){
        while(true){
            Debug.Log(s);
            //Increase slider value
            forceSliderO.GetComponent<Slider>().value++;
            //Increase force
            yield return null;
        }
    }

}
