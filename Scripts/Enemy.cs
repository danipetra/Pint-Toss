using UnityEngine;
using System.Collections;

public class Enemy : Opponent
{
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        //if(pint.GetComponent<Pint>().canBeThrown)
            StartCoroutine(PerformThrow());
    }

    private IEnumerator PerformThrow(){
        float force = Mathf.Abs( Utils.RandomGaussian(0, 1) );
        float delay = force + Mathf.Abs( Utils.RandomGaussian(0, force) );
        Debug.Log("Enemy choice: " + force);
        yield return new WaitForSeconds(delay);
        
        ThrowPint(force);
        // wait for the time corrensponding to that action until the next throw
    }
}
