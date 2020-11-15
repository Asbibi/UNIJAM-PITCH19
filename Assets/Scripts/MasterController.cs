﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    [SerializeField] Transform startPosition = null;
    [SerializeField] Transform endPosition = null;
    public ViewBoxScript viewBox;
    private float waitTime = 0;
    private float currentTime = 0;
    private float travelTime = 1;
    private bool started = false;

    public void InitTimes(float _waitTime, float _travelTime)
    {
        waitTime = _waitTime;
        travelTime = _travelTime;
        transform.position = startPosition.position;
        StartCoroutine(waitBeforeStarting(waitTime));
    }
    public void Stop()
    {
        started = false;
    }
    public void Resume()
    {
        GetComponent<Animator>().SetBool("idle", true);
        StartCoroutine(waitBeforeStarting(3));
        GetComponent<Animator>().SetBool("idle", false);
    }

    private void Update()
    {
        if (started)
        {
            currentTime += Time.deltaTime;
        }
        transform.position = Vector3.Lerp(startPosition.position, endPosition.position, (currentTime / travelTime));
        if (currentTime >= travelTime)
        {
            GameManager.EndGame();
            started = false;
        }

        //Si le maitre voit le joueur
        if (viewBox.isSeeingPlayer())
        {
            //On demande au game manager de check les états du joueur par rapport au maitre
            GameManager.MasterChecking();
        }
    }

    IEnumerator waitBeforeStarting(float waitTimeSetting)
    {
        yield return new WaitForSeconds(waitTimeSetting);
        started = true;
    }

}
