using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interaction : MonoBehaviour
{
    [SerializeField]
    private int animationTime = 1;
    [SerializeField]
    protected int pointsGiven = 10;

    private PlayerControler playerControler;
    protected bool interactible = true;

    // Start is called before the first frame update
    void Start()
    {
        playerControler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();
        if (playerControler == null)
        {
            Debug.Log("Cant find gamecontroller");
        }
    }

    public abstract void Interact();

    protected IEnumerator WaitForAnimation()
    {
        playerControler.GetComponent<PlayerControler>().LockPlayer();

        //Print the time of when the function is first called.
        Debug.Log("Started Interaction at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(animationTime * Time.deltaTime);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Interaction at timestamp : " + Time.time);
        playerControler = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControler>();

        playerControler.GetComponent<PlayerControler>().FreePlayer();
    }
}
