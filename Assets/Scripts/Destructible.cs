using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private int numberOfCoins;

    public void Start()
    {
        
    }

    public void TakeDamage()
    {
        Debug.Log("dropped " + numberOfCoins + " coins");
        gameObject.SetActive(false);
    }

}
