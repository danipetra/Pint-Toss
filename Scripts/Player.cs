using UnityEngine;
using UnityEngine.UI;

public class Player : Opponent{

    private GameObject forceSliderObj;
    
    new void Awake()
    {
        base.Awake();

        forceSliderObj = GameObject.Find("Force Slider");
        if(!forceSliderObj) Debug.LogError("Force Slider GameObject not found");
    }

    new void Update()
    {
        base.Update();
    }

    public new void PickPint()
    {
        base.PickPint();
        forceSliderObj.GetComponent<Slider>().value = 0;
    }

}
