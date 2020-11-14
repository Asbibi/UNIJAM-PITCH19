using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionEatable : Interaction
{
    public override void Interact()
    {
        if (interactible)
        {
            Debug.Log("Miam miam");

            StartCoroutine(WaitForAnimation());

            this.GetComponent<SpriteRenderer>().flipX;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
}
