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
    private LayerMask ladderLayerM;


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
        TurnAround();
    }

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
        if (canMove)
        {
            playerRB.velocity = new Vector2(InputSpeed.x * playerSpeed * Time.deltaTime, playerRB.velocity.y);
        }
    }

    public void Interact()
    {
    }

    public void SetInputSpeed(Vector2 InputSpeed)
    {
        this.InputSpeed = InputSpeed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag);
    }

}
