using UnityEngine;
using System.Collections;

public class Enemy : Opponent
{
    private bool canThrow;
    private float transparency = .01f;
    
    new void Awake()
    {
        base.Awake();
        canThrow = true;
        Color enemyColor = Color.red;
        enemyColor.a = 0.9f;
        Utils.GetChildWithName(gameObject, "Pint").GetComponentInChildren<MeshRenderer>().material.color = enemyColor;
    }

    new void Update()
    {
        base.Update();
        if(canThrow && pint.GetComponent<Pint>().canBeThrown)
            StartCoroutine(PerformThrow());
    }

    private IEnumerator PerformThrow()
    {
        canThrow = false;
        float force = Mathf.Abs( Utils.RandomGaussian(0, 1) );
        float delay = force + Mathf.Abs( Utils.RandomGaussian(0, force) );

        yield return new WaitForSeconds(delay);
        ThrowPint(force);
        canThrow = true;
    }
}
