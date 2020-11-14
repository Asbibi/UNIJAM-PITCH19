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



    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float ladderSpeed = 2f;
    [SerializeField]
    private GameObject currentInteractableObject = null;


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
        MoveHorizontal();
    }

    #region Movement Horizontal
    private void MoveHorizontal()
    {
        if (canMove)
        {
            playerRB.velocity = new Vector2(InputSpeed.x * playerSpeed * Time.deltaTime, playerRB.velocity.y);
            if (InputSpeed.x > 0)
            {
                facingDirection = 1;
            }
            else if (InputSpeed.x < 0)
            {
                facingDirection = -1;
            }
        }
    }
    public void SetInputSpeed(Vector2 InputSpeed)
    {
        this.InputSpeed = InputSpeed;
    }
    #endregion

    #region Interactions Base
    public void Interact()
    {
        if (currentInteractableObject.GetComponent<InteractionLadder>() != null)
        {
            StartCoroutine(Ladder(transform.position.y < 0, currentInteractableObject.GetComponent<InteractionLadder>().height));
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
    }
    #endregion
}
