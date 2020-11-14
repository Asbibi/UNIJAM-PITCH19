using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewBoxScript : MonoBehaviour
{
    public bool seePlayer = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            seePlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            seePlayer = false;
        }
    }

    public bool isSeeingPlayer()
    {
        return seePlayer;
    }
}
