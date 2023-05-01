/*TODO: manage backboard text and bonuses only here instead of using the GameManager*/
using UnityEngine;
using TMPro;
using System.Collections;

public class Backboard : MonoBehaviour
{
    [SerializeField, Range(0f , 1f)] private float blinkChance = 0.0003f;
    [SerializeField, Range(5 , 15)] private int blinkTime = 5;
    private bool isBlinking;
    
    private TMP_Text text;
    private Color defaultTextColor;

    private void Awake() 
    {
        isBlinking = false;
        text = GetComponentInChildren<TMP_Text>();
        defaultTextColor = text.color;
    }

    private void FixedUpdate()
    {
        if(!isBlinking && Random.Range(0f, 1f) < blinkChance )
        {
            //Debug.LogWarning("Blink ! " );
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;
        text.color = Color.blue;
        text.text = "x4";
        text.fontSize++;

        yield return new WaitForSeconds(blinkTime); 
        
        text.text = "x2";
        text.fontSize--;
        isBlinking = false;
        text.color = defaultTextColor;
    }

    public bool IsBlinking() => isBlinking;
}
