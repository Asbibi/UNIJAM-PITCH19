﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField]
    private float smoothTime;
    private float speed;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3( Mathf.SmoothDamp(transform.position.x, player.transform.position.x,ref speed, smoothTime), player.transform.position.y, transform.position.z);
    }
}
