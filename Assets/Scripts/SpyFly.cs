using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyFly : Spy
{
    [SerializeField] private float visionDistance = 3;
    [SerializeField] Transform visionTransform = null;

    private void Awake()
    {
        visionTransform.localScale = Vector3.one * (visionDistance / 3);
    }

    override protected void OnTroubleNotified(Vector3 playerPosition)
    {
        if (Vector3.Distance(playerPosition, transform.position) < visionDistance)
        {
            Report();
        }
    }
}
