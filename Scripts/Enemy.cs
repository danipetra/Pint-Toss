using UnityEngine;
using System.Collections;

public class Enemy : Opponent
{
    private bool _canThrow;
    private float _transparency = .01f;
    
    new void Awake()
    {
        base.Awake();
        _canThrow = true;
        Color enemyColor = Color.red;
        enemyColor.a = _transparency;
        Utils.GetChildWithName(gameObject, "Pint").GetComponentInChildren<MeshRenderer>().material.color = enemyColor;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        if(_canThrow && pint.GetComponent<Pint>().canBeThrown)
            StartCoroutine(PerformThrow());
    }

    private IEnumerator PerformThrow()
    {
        _canThrow = false;
        float force = Mathf.Abs( Utils.RandomGaussian(0, 1) );
        float delay = force + Mathf.Abs( Utils.RandomGaussian(0, force) );

        yield return new WaitForSeconds(delay);
        ThrowPint(force);
        _canThrow = true;
    }

    
}
