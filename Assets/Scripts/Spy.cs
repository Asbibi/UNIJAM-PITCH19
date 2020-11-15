using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour
{
    [SerializeField] int sanctionScore = 1;
    [SerializeField] GameObject exclamationPoint = null;

    // Start is called before the first frame update
    virtual protected void Start()
    {
        GameManager.onTroubleDone += OnTroubleNotified;
    }

    virtual protected void OnTroubleNotified(Vector3 playerPosition)
    {
        Debug.LogWarning("Player has caused troubles");
        Report();
    }

    protected void spawnExclamation()
    {
        Instantiate(exclamationPoint, transform.position + Vector3.up * 2, Quaternion.identity);
    }

    protected void Report()
    {
        spawnExclamation();
        GameManager.ReportPlayer(sanctionScore);
        FindObjectOfType<AudioManager>().Play("spy");
    }
}
