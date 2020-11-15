using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyMage : SpyLinear
{
    [SerializeField] private float uTurnDelay = 2;
    [SerializeField] Animator mageAnimator;
    float timerUturn = 0;


    private void Update()
    {
        timerUturn += Time.deltaTime;
        if (timerUturn > uTurnDelay)
        {
            facingRight = !facingRight;

            if (facingRight)            
                transform.rotation = Quaternion.Euler(0, 0, 0);            
            else 
                transform.rotation = Quaternion.Euler(0, 180, 0);

            timerUturn = 0;
        }
    }

    public void Init()
    {
        timerUturn = 0;
        mageAnimator.SetBool("Pop", true);
    }

    public void Disable()
    {
        mageAnimator.SetBool("Pop", false);
        StartCoroutine(DisableAnim());
    }
    IEnumerator DisableAnim()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
