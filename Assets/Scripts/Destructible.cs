using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private int maxNumberOfCoins = 0;
    [SerializeField]
    private GameObject coinPF = null;
    public void Start()
    {
        
    }

    public void TakeDamage()
    {
        int NbCoins = Random.Range(0,maxNumberOfCoins);
        Debug.Log("dropping " + NbCoins + " coins");
        gameObject.SetActive(false);
    }

}
