using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag("Destructible"))
        {
            Debug.Log(target.tag);
            if (target.GetComponent<Destructible>() == null)
            {
                Debug.Log("null");
            }
            target.GetComponent<Destructible>().TakeDamage();
        }
    }
}
