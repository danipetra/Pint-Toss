using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Player : Opponent{

    private GameObject forceSliderObj;
    private TMP_Text pointsText;
    
    new void Awake()
    {
        base.Awake();

        forceSliderObj = GameObject.Find("Force Slider");
        if(!forceSliderObj) Debug.LogError("Force Slider GameObject not found");

        pointsText = Utils.GetChildWithName(gameObject, "Points Text").GetComponent<TMP_Text>();
        pointsText.gameObject.SetActive(false);
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public new void PickPint()
    {
        base.PickPint();
        forceSliderObj.GetComponent<Slider>().value = 0;
    }

    public void ShowPoints(int points){
        
        float time = .4f;
        StartCoroutine(ActivatePointsText(points, time));
    }

    private IEnumerator ActivatePointsText(int points, float time){
        pointsText.gameObject.SetActive(true);
        pointsText.text = " + "+ points;

        yield return new WaitForSeconds(time);

        pointsText.gameObject.SetActive(false);
    }
}
