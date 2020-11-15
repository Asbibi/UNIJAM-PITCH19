using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleCrow : Destructible
{
    [SerializeField]
    private int basePoints = 0;

    override public void TakeDamage()
    {
        if (interactable)
        {
            //Debug.Log("Crow Stuned");            
            GetComponent<SpyCrow>().Stunned();

            
            int NbPoints = Random.Range(0, points) + basePoints;
            GameManager.NotifyTroubleDone(NbPoints);

            //FindObjectOfType<AudioManager>().Play("vase");
            interactable = false;
        }
    }    
}
