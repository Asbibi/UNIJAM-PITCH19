using System.Collections;
using UnityEngine;

public class EexclamatioPopUp : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(destr());
    }


    IEnumerator destr()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
