using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChangeSprite : Interaction
{
    [SerializeField]
    private Sprite eaten;
    private SpriteRenderer spriteRenderer;
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }


    public override void Interact()
    {
        if (interactible)
        {
            Debug.Log("Miam miam");

            spriteRenderer.sprite = eaten;

            StartCoroutine(WaitForAnimation());

            int NbPoints = Random.Range(0, pointsGiven);

            Debug.Log("adding " + NbPoints + " points");

            interactible = false;

            GameManager.NotifyTroubleDone(NbPoints);
        }
        
    }

    
}
