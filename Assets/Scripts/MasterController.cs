using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    [SerializeField] Transform startPosition = null;
    [SerializeField] Transform endPosition = null;
    private float waitTime = 0;
    private float currentTime = 0;
    private float travelTime = 1;
    private bool started = false;

    public void InitTimes(float _waitTime, float _travelTime)
    {
        waitTime = _waitTime;
        travelTime = _travelTime;
        transform.position = startPosition.position;
        StartCoroutine(waitBeforeStarting());
    }
    public void Stop()
    {
        started = false;
    }
    public void Resume()
    {
        StartCoroutine(waitBeforeStarting());
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
            Debug.Log("Game is Over");
            started = false;
        }

        //Check position joueur par le game manager
        GameManager.
    }

    IEnumerator waitBeforeStarting()
    {
        yield return new WaitForSeconds(waitTime);
        started = true;
    }

}
