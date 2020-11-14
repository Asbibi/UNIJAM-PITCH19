﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spy : MonoBehaviour
{
    // Start is called before the first frame update
    virtual protected void Start()
    {
        GameManager.onTroubleDone += OnTroubleNotified;
    }

    void OnTroubleNotified()
    {
        Debug.LogWarning("Player seen causing troubles");
    }
}
