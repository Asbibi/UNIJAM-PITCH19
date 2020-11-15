using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionChangeSprite : Interaction
{
    [SerializeField]
    private Sprite modifiedSprite;
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
            if(GetComponent<AudioSource>() != null)
            {
                GetComponent<AudioSource>().Play();
            }

            spriteRenderer.sprite = modifiedSprite;

            StartCoroutine(WaitForAnimation());

            int NbPoints = Random.Range(0, pointsGiven);

            Debug.Log("adding " + NbPoints + " points");

            interactible = false;

            if (transform.Find("Outline") != null && transform.Find("Outline").GetComponent<SpriteRenderer>() != null)
            {
                transform.Find("Outline").GetComponent<SpriteRenderer>().enabled = false;
            }
            if (transform.Find("AnimationToDisable")){
                transform.Find("AnimationToDisable").gameObject.SetActive(false);
            }


            GameManager.NotifyTroubleDone(NbPoints);
        }
        
    }

    
}
