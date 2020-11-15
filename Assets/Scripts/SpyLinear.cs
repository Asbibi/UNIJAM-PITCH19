using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyLinear : Spy
{
    [SerializeField] private float visionDistance = 5;
    [SerializeField] private Transform visionPivot = null;
    [SerializeField] protected bool facingRight = true;


    private void Awake()
    {
        visionPivot.localScale = Vector3.one * visionDistance;
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
}
