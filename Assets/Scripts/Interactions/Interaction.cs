using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [SerializeField]
    private int animationTime;
    private GameObject player;
    protected bool interactible = true;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public abstract void Interact();

    protected IEnumerator WaitForAnimation()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Interaction at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(animationTime * Time.deltaTime);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Interaction at timestamp : " + Time.time);

        player.GetComponent<PlayerControler>().FreePlayer();
    }
}
