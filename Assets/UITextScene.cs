using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITextScene : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Interact") || Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(GoToNextScene());
        }
    }

    IEnumerator GoToNextScene()
    {
        GetComponent<Animator>().SetBool("Next", true);
        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("FinalScene");
    }
}
