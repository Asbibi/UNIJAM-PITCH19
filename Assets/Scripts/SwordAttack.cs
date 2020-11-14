using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Destructible"))
        {
            Debug.Log("sos");
            target.GetComponent<Destructible>().TakeDamage();
        }
    }
}
