using UnityEngine;
using System.Collections;

public class Enemy : Opponent
{
    private bool canThrow;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        canThrow = true;
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //if(canThrow && pint.GetComponent<Pint>().canBeThrown)
          //  StartCoroutine(PerformThrow());
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
