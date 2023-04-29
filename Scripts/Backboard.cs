using UnityEngine;
using TMPro;
using System.Collections;

public class Backboard : MonoBehaviour
{
    [SerializeField, Range(0f , 1f)] private float blinkChance = 0.0003f;
    [SerializeField, Range(5 , 15)] private int blinkTime = 5;
    private bool isBlinking;
    private TMP_Text text;

    private void Awake() 
    {
        isBlinking = false;
        text = GetComponentInChildren<TMP_Text>();
    }

    private void FixedUpdate()
    {
        if(!isBlinking && Random.Range(0f, 1f) < blinkChance )
        {
            Debug.LogWarning("Blink ! " );
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;
        text.color = Color.red;

        yield return new WaitForSeconds(blinkTime); 
        
        isBlinking = false;
        text.color = Color.white;
    }

    public bool IsBlinking() => isBlinking;
}
