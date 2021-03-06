﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;
    private Animator animator;

    private Vector2 InputSpeed;
    private int facingDirection;
    private bool canMove;
    private bool attacking;
    private bool walledR = false;
    private bool walledL = false;

    [SerializeField]
    private GameObject sword1;
    [SerializeField]
    private GameObject sword2;
    private float playerSpeed = 200;


    [Header("Interactions")]
    private GameObject currentInteractableObject = null;
    [SerializeField]
    private float ladderSpeed = 2f;
    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float jumpDuration = 1f;
    [SerializeField]
    private int attackFrames = 17;
    private int currentAttackFrames;
    [SerializeField]
    private Text actionText;

    //ajout Paul1
    public Transform replacePosition = null;
    private bool replacement = false;
    public bool replacing = false;
    private float positionYBase;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        facingDirection = 1;
        canMove = true;
        attacking = false;
        //ajout Paul1
        positionYBase = transform.position.y;
        actionText.enabled = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            MoveHorizontal();
            TurnAround();
        }
        else
        {
            playerRB.velocity = Vector2.zero;
            animator.SetBool("walkin", false);
        }
            AttackTimer();

        if (replacement)
        {
            replacementAnim();
        }
    }



    #region Movement Horizontal
    private void TurnAround()
    {
        if (InputSpeed.x > 0 && facingDirection == -1)
        {
            facingDirection = 1;
            transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (InputSpeed.x < 0)
        {
            facingDirection = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    private void MoveHorizontal()
    {
        if (walledR && InputSpeed.x > 0)
            InputSpeed.x = 0;
        else if (walledL && InputSpeed.x < 0)
            InputSpeed.x = 0;
        playerRB.velocity = new Vector2(InputSpeed.x * playerSpeed * Time.deltaTime, playerRB.velocity.y);
        animator.SetBool("walkin", Mathf.Abs(InputSpeed.x) > 0);
    }
    public void SetInputSpeed(Vector2 InputSpeed)
    {
        this.InputSpeed = InputSpeed;
    }
    #endregion

    #region SwordAttack
    private void AttackTimer()
    {
        if (attacking)
        {
            currentAttackFrames++;
            if (currentAttackFrames > attackFrames *3/4){
                sword2.SetActive(true);
            }
            if (currentAttackFrames > attackFrames)
            {
                StopSwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        if (!attacking)
        {
            currentAttackFrames = 0;
            sword1.SetActive(true);
            attacking = true;
            animator.SetBool("isAttacking", true);
            FindObjectOfType<AudioManager>().Play("epee");
        }
    }


    public void StopSwordAttack()
    {
        sword1.SetActive(false);
        sword2.SetActive(false);
        attacking = false;
        animator.SetBool("isAttacking", false);
    }
    #endregion

    #region Interactions Base
    public void Interact()
    {
        if (currentInteractableObject != null && canMove)
        {
            if (currentInteractableObject.GetComponent<InteractionLadder>() != null)
            {
                StartCoroutine(Ladder(currentInteractableObject.GetComponent<Collider2D>().bounds.center.y > transform.position.y, currentInteractableObject.GetComponent<Collider2D>()));
            }
            else if (currentInteractableObject.GetComponent<InteractionBalcon>() != null)
            {
                StartCoroutine(BalconJump(currentInteractableObject.transform.position.y, currentInteractableObject.GetComponent<InteractionBalcon>().GetOtherPointPosition()));                
            }
            else if (currentInteractableObject.GetComponent<Interaction>() != null)
            {
                currentInteractableObject.GetComponent<Interaction>().Interact();
            }
            else if(currentInteractableObject.GetComponent<InteractionEndLevel>() != null)
            {
                Debug.Log("EndGame");
                Destroy(currentInteractableObject.GetComponent<InteractionEndLevel>());
                GameManager.EndGameByPlayer();
            }
        }
    }

    public void FreePlayer()
    {
        canMove = true;
        animator.SetBool("interacting", false);
    }
    
    public void LockPlayer()
    {
        canMove = false;
        animator.SetBool("interacting", true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string colTag = col.gameObject.tag;
        //Debug.Log(colTag + " " + col.gameObject.name);
        if (colTag == "Interactable")
        {
            currentInteractableObject = col.gameObject;
            if (currentInteractableObject.transform.Find("Outline") != null && currentInteractableObject.transform.Find("Outline").GetComponent<SpriteRenderer>() != null && currentInteractableObject.GetComponent<Interaction>().isInteractible() == true)
            {
                currentInteractableObject.transform.Find("Outline").GetComponent<SpriteRenderer>().enabled = true;
            }
            actionText.enabled = true;
        }
        else if (colTag == "Wall")
        {
            if (col.transform.position.x < transform.position.x)
                walledL = true;
            else if (col.transform.position.x > transform.position.x)
                walledR = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        //Debug.LogWarning("exit " + col.gameObject.tag);

        if (col.transform.Find("Outline") != null && col.transform.Find("Outline").GetComponent<SpriteRenderer>() != null)
        {
            col.transform.Find("Outline").GetComponent<SpriteRenderer>().enabled = false;
        }

        if (col.gameObject == currentInteractableObject)
        {
            currentInteractableObject = null;
            actionText.enabled = false;
        }
        else if (col.gameObject.tag == "Wall")
        {
            walledR = false;
            walledL = false;
        }
    }
    #endregion

    #region Interactions Other
    IEnumerator Ladder(bool up, Collider2D col)
    {
        canMove = false;
        float _currentHeight = 0;
        float dif = transform.position.x - col.bounds.center.x;
        while (Mathf.Abs(dif) > 0.1f)
        {
            transform.position = new Vector2(transform.position.x - (dif > 0 ? 0.02f : -0.02f)*playerSpeed*Time.deltaTime, transform.position.y);
            dif = transform.position.x - col.bounds.center.x;
            yield return null;
        }
        while (_currentHeight < col.bounds.size.y - 0.2f)
        {
            animator.SetBool("onLadder", true);
            _currentHeight += ladderSpeed * Time.deltaTime;
            transform.position += Vector3.up * ladderSpeed * Time.deltaTime * (up ? 1 : -1);
            yield return null;
        }
        animator.SetBool("onLadder", false);
        canMove = true;
    }
    IEnumerator BalconJump(float yStart, Vector3 positionEnd)
    {
        canMove = false;

        // Initialisation
        float _timer = 0;
        Vector3 _positionStart = transform.position;
        positionEnd.y += _positionStart.y - yStart;
        animator.SetBool("jumping", true);

        //Rotation
        if (positionEnd.x < _positionStart.x)  // on va vers la gauche
        {
            facingDirection = -1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            facingDirection = 1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        // Jump
        while (_timer < jumpDuration)
        {
            float x = _timer / jumpDuration;
            transform.position = Vector3.Lerp(_positionStart, positionEnd, x);
            transform.position += Vector3.up * (jumpHeight *(-4*x*x + 4*x));

            _timer += Time.deltaTime;
            yield return null;
        }

        if (facingDirection == 1)
            walledR = false;
        else
            walledL = false;

        transform.position = positionEnd;

        animator.SetBool("jumping", false);
        canMove = true;
    }
    #endregion

    #region Replacement joueur
    public void setCanMove(bool canMoveSetting)
    {
        canMove = canMoveSetting;
    }

    public void setReplacement(bool replacementSetting)
    {
        replacement = replacementSetting;
    }
    public bool getReplacement()
    {
        return replacement;
    }
    public void replacementAnim()
    {
        if (transform.position.y != positionYBase)
        {
            transform.position = new Vector3(transform.position.x, positionYBase, transform.position.z);
        }
        facingDirection = 1;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        StartCoroutine(waitBeforeDeplacement());
    }

    IEnumerator waitBeforeDeplacement()
    {
        yield return new WaitForSeconds(1);
        transform.position = Vector3.MoveTowards(transform.position, replacePosition.position, 10 * Time.deltaTime);
        setReplacement(false);
        replacing = true;
        yield return new WaitForSeconds(1);
        replacing = false;
        setCanMove(true);
    }
    #endregion
}
