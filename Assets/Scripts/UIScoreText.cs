using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreText : MonoBehaviour
{
    public void Init(string txt, Camera cam, Vector3 playerTransform)
    {
        GetComponentInChildren<Text>().text = txt;
        GetComponent<RectTransform>().position = cam.WorldToScreenPoint(playerTransform);
        StartCoroutine(kill());
    }

    IEnumerator kill()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
