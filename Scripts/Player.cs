using UnityEngine;
using UnityEngine.UI;

public class Player : Opponent{

    private GameObject forceSliderObj;
    
    new void Start()
    {
        base.Start();

        forceSliderObj = GameObject.Find("Force Slider");
        if(!forceSliderObj) Debug.LogError("Force Slider GameObject not found");
    }

    new void Update()
    {
        base.Update();
    }

    /* TODO: try to move it to InputManager */
    

    new public void Respawn(){
        base.Respawn();
        forceSliderObj.GetComponent<Slider>().value = 0;
    }

    public GameObject GetSlider(){
        return forceSliderObj;
    }

}
