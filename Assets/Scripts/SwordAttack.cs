using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerStay2D(Collider2D target)
    {
        //Debug.Log("Colide");
        if (target.CompareTag("Destructible"))
        {
            //Debug.Log("sos");
            target.GetComponent<Destructible>().TakeDamage();
        }
    }
}
