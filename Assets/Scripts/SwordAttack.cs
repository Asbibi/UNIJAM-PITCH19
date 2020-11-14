using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Destructible"))
        {
            target.GetComponent<Destructible>().TakeDamage();
        }
    }
}
