using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyGuard : SpyLinear
{
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;

    [SerializeField] private float travelTime = 2;
    private float timerUturn = 0;


    private void Update()
    {
        if (facingRight)
            timerUturn += Time.deltaTime;
        else
            timerUturn -= Time.deltaTime;

        if (timerUturn > travelTime || timerUturn < 0)
        {
            if (timerUturn < 0)
                facingRight = true;
            else
                facingRight = false;

            if (facingRight)
                transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        transform.position = Vector3.Lerp(pointA.position, pointB.position, timerUturn/travelTime);
    }
}
