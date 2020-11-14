using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyMage : Spy
{
    [SerializeField] private float visionDistance = 5;
    [SerializeField] private Transform visionPivot = null;
    private bool facingRight = true;

    [SerializeField] private float uTurnDelay = 2;
    float timerUturn = 0;

    private void Awake()
    {
        visionPivot.localScale = Vector3.one * visionDistance;
    }


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

    override protected void OnTroubleNotified(Vector3 playerPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, visionDistance);
        //Debug.DrawRay(transform.position, transform.right * visionDistance, Color.white);
        if (hit)
        {
            if (hit.collider.tag == "Player")
            {
                Report();
            }
        }
    }

    public void ResetUturnTimer()
    {
        timerUturn = 0;
    }
}
