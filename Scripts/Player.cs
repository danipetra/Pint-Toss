using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Player : Opponent{

    private GameObject _forceSliderObj;

    private TMP_Text _pointsText;
    private float _pointsTextActiveTime = .4f;

    new void Awake()
    {
        base.Awake();

        _forceSliderObj = GameObject.Find("Force Slider");
        if(!_forceSliderObj) Debug.LogError("Force Slider GameObject not found");

        _pointsText = Utils.GetChildWithName(gameObject, "Points Text").GetComponent<TMP_Text>();
        _pointsText.gameObject.SetActive(false);
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public new void PickPint()
    {
        base.PickPint();
        _forceSliderObj.GetComponent<Slider>().value = 0;
    }

    public void ShowPoints(int points)
    {
        StartCoroutine(ActivatePointsText(points, _pointsTextActiveTime));
    }

    private IEnumerator ActivatePointsText(int points, float time)
    {
        _pointsText.gameObject.SetActive(true);
        _pointsText.text = " + "+ points;

        yield return new WaitForSeconds(time);

        _pointsText.gameObject.SetActive(false);
    }
}
