using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    protected int points = 0;
    [SerializeField]
    private Sprite broken;
    private SpriteRenderer spriteRenderer;
    protected bool interactable = true;

    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    virtual public void TakeDamage()
    {
        if (interactable)
        {
            int NbPoints = Random.Range(0, points);
            GameManager.NotifyTroubleDone(NbPoints);
            spriteRenderer.sprite = broken;
            FindObjectOfType<AudioManager>().Play("vase");
            interactable = false;
        }
    }

}
