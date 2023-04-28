using UnityEngine;
using System.Collections;

public class Enemy : Opponent
{
    private bool canThrow;
    
    new void Awake()
    {
        base.Awake();
        canThrow = true;
    }

    private void Start() {
        //GetComponentInChildren<MeshRenderer>().material.color = new Color(1f, 1f, 1f, 0.3f);
    }

    new void Update()
    {
        base.Update();
        if(canThrow && pint.GetComponent<Pint>().canBeThrown)
            StartCoroutine(PerformThrow());
    }

    private IEnumerator PerformThrow(){
        canThrow = false;
        float force = Mathf.Abs( Utils.RandomGaussian(0, 1) );
        float delay = force + Mathf.Abs( Utils.RandomGaussian(0, force) );

        yield return new WaitForSeconds(delay);
        ThrowPint(force);
        canThrow = true;
    }
}
