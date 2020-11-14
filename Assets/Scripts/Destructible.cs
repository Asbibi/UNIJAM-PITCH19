using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField]
    private int points = 0;
    [SerializeField]
    public Sprite broken;
    public SpriteRenderer spriteRenderer;
    public void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void TakeDamage()
    {
        int NbPoints = Random.Range(0, points);
        Debug.Log("adding " + NbPoints + " points");
        FindObjectOfType<AudioManager>().Play("vase");
        spriteRenderer.sprite = broken;
    }

}
