using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour
{
    [SerializeField] int sanctionScore = 1;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.onTroubleDone += OnTroubleNotified;
    }

    virtual protected void OnTroubleNotified(Vector3 playerPosition)
    {
        Debug.LogWarning("Player has caused troubles");
        Report();
    }

    protected void Report()
    {
        GameManager.ReportPlayer(sanctionScore);
    }
}
