/*TODO: manage backboard text and bonuses only here instead of using the GameManager*/
using UnityEngine;
using TMPro;
using System.Collections;

public class Backboard : MonoBehaviour
{
    [SerializeField, Range(0f , 1f)] private float _blinkChance = 0.0003f;
    [SerializeField, Range(5 , 15)] private int _blinkTime = 5;
    private bool _isBlinking;
    
    private TMP_Text _text;
    private Color _defaultTextColor;

    private void Awake() 
    {
        _isBlinking = false;
        _text = GetComponentInChildren<TMP_Text>();
        _defaultTextColor = _text.color;
    }

    private void FixedUpdate()
    {
        if(!_isBlinking && Random.Range(0f, 1f) < _blinkChance )
        {
            //Debug.LogWarning("Blink ! " );
            StartCoroutine(Blink());
        }
    }

    private IEnumerator Blink()
    {
        _isBlinking = true;
        _text.color = Color.blue;
        _text.text = "x4";
        _text.fontSize++;

        yield return new WaitForSeconds(_blinkTime); 
        
        _text.text = "x2";
        _text.fontSize--;
        _isBlinking = false;
        _text.color = _defaultTextColor;
    }

    public bool IsBlinking() => _isBlinking;
}
