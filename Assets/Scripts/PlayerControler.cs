using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;


    private Vector2 InputSpeed;
    private int facingDirection;
    private bool canMove;
    private bool walledR = false;
    private bool walledL = false;



    [SerializeField]
    private float playerSpeed;

    [Header("Interactions")]
    private GameObject currentInteractableObject = null;
    [SerializeField]
    private float ladderSpeed = 2f;
    [SerializeField]
    private float jumpHeight = 1f;
    [SerializeField]
    private float jumpDuration = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();
        facingDirection = 1;
        canMove = true;
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
            playerRB.velocity = Vector2.zero;
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
    }
    public void SetInputSpeed(Vector2 InputSpeed)
    {
        this.InputSpeed = InputSpeed;
    }
    #endregion

    #region Interactions Base
    public void Interact()
    {
        if (currentInteractableObject != null && canMove)
        {
            canMove = false;


            if (currentInteractableObject.GetComponent<InteractionLadder>() != null)
            {
                StartCoroutine(Ladder(transform.position.y < 0, currentInteractableObject.GetComponent<InteractionLadder>().height));
            }
            else if (currentInteractableObject.GetComponent<InteractionBalcon>() != null)
            {
                StartCoroutine(BalconJump(currentInteractableObject.transform.position, currentInteractableObject.GetComponent<InteractionBalcon>().GetOtherPointPosition()));                
            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        string colTag = col.gameObject.tag;
        Debug.Log(colTag);
        if (colTag == "Interactable")
        {
            currentInteractableObject = col.gameObject;
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
        if (col.gameObject == currentInteractableObject)
            currentInteractableObject = null;
        else if (col.gameObject.tag == "Wall")
        {
            walledR = false;
            walledL = false;
        }
    }
    #endregion

    #region Interactions Other
    IEnumerator Ladder(bool up, float height)
    {
        float _currentHeight = 0;
        int upInt = -1;
        if (up)
            upInt = 1;
        while (_currentHeight < height)
        {
            _currentHeight += ladderSpeed * Time.deltaTime; //ladder speed;
            transform.position += Vector3.up * ladderSpeed * Time.deltaTime * upInt;
            yield return null;
        }

        canMove = true;
    }
    IEnumerator BalconJump(Vector3 positionStart, Vector3 positionEnd)
    {
        float _timer = 0;
        while (_timer < jumpDuration)
        {
            float x = _timer / jumpDuration;
            transform.position = Vector3.Lerp(positionStart, positionEnd, x);

            /*
            if (_timer < jumpDuration / 2)
                transform.position += Vector3.up * (jumpHeight * _timer / jumpDuration);
            else
                transform.position += Vector3.up * (jumpHeight * (1 - _timer / jumpDuration));
            */
            transform.position += Vector3.up * (jumpHeight *(-4*x*x + 4*x));

            _timer += Time.deltaTime;
            yield return null;
        }
        canMove = true;
    }
    #endregion
}
