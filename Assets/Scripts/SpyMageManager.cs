using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyMageManager : MonoBehaviour
{
    [SerializeField] Transform mage = null;
    [SerializeField] Transform player = null;
    [SerializeField] float delaySpawn = 5;
    [SerializeField] float durationSpawn = 3;
    [SerializeField] float distanceValidSpawn = 3;

    bool spawned = false;
    bool waiting = false;

    [SerializeField] Transform[] mageSpawnPoints = new Transform[0];

    // Start is called before the first frame update
    void Start()
    {
        mage.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (!waiting)
        {
            waiting = true;
            if (spawned)
            {
                StartCoroutine(waitToDespawn());
            }
            else
                StartCoroutine(waitToSpawn());
        }
    }


    void spawnMage()
    {
        int idSpawnPoint = Random.Range(0, mageSpawnPoints.Length);
        int idSup = mageSpawnPoints.Length;
        int idInf = 0;
        while (idSup > (idInf+1) && Mathf.Abs(mageSpawnPoints[idSpawnPoint].position.x - player.position.x) > distanceValidSpawn)
        {
            if (mageSpawnPoints[idSpawnPoint].position.x - player.position.x > 0)
                idSup = idSpawnPoint;
            else
                idInf = idSpawnPoint;
            idSpawnPoint = Random.Range(idInf, idSup);
        }
        mage.position = mageSpawnPoints[idSpawnPoint].position;
        mage.gameObject.SetActive(true);
        spawned = true;
    }

    IEnumerator waitToSpawn()
    {
        yield return new WaitForSeconds(delaySpawn);
        spawnMage();
        waiting = false;
    }
    IEnumerator waitToDespawn()
    {
        yield return new WaitForSeconds(durationSpawn);
        mage.gameObject.SetActive(false);
        spawned = false;
        waiting = false;
    }
}
