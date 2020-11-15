using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyCircle : Spy
{
    [SerializeField] protected float visionDistance = 3.15f;
    [SerializeField] Transform visionTransform = null;

    private void Awake()
    {
        visionTransform.localScale = Vector3.one * (visionDistance*35 / 3.15f);
    }

    override protected void OnTroubleNotified(Vector3 playerPosition)
    {
        if (Vector3.Distance(playerPosition, transform.position) < visionDistance)
        {
            Report();
        }
    }

    


}
