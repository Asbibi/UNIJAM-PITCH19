using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpyCrow : SpyCircle
{
    enum CROWSTATE { OBSERVING, REPORTING, STUNED };
    CROWSTATE state = CROWSTATE.OBSERVING;

    Transform masterTransform;
    [SerializeField] Animator animator = null;

    [SerializeField] float leaveGroundTime = 1;
    [SerializeField] float flySpeed = 3;

    override protected void Start()
    {
        base.Start();
        masterTransform = GameManager.GetMasterTransform();
    }


    override protected void OnTroubleNotified(Vector3 playerPosition)
    {
        if (state == CROWSTATE.OBSERVING && Vector3.Distance(playerPosition, transform.position) < visionDistance)
        {
            StartCoroutine(flyYouFool());
            spawnExclamation();
            //Report();
        }
    }

    IEnumerator flyYouFool()
    {
        animator.SetBool("OnGround", false);
        yield return new WaitForSeconds(leaveGroundTime);
        transform.position += Vector3.up;
        animator.SetBool("Flying", true);
        //Orienter
        if (masterTransform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        state = CROWSTATE.REPORTING;
    }

    private void Update()
    {
        if (state == CROWSTATE.REPORTING)
        {
            if (Mathf.Abs(masterTransform.position.x - transform.position.x) < 1)
            {
                Report();
                Destroy(gameObject);
            }
            else if (masterTransform.position.x < transform.position.x)
            {
                transform.position += Vector3.left * flySpeed * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * flySpeed * Time.deltaTime;
            }
        }
    }


    public void Stunned()
    {
        StopAllCoroutines();
        if (state == CROWSTATE.REPORTING)
        {
            transform.position += Vector3.down;
        }
        state = CROWSTATE.STUNED;
        animator.SetBool("Stuned", true);
    }
}