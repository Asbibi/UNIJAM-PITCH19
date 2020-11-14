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
    private bool attacking;

    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private LayerMask ladderLayerM;
    [SerializeField]
    private int attackFrames;
    private int currentAttackFrames;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();
        facingDirection = 1;
        canMove = true;
        attacking = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHorizontal();
        TurnAround();
        AttackTimer();
    }

    private void AttackTimer()
    {
        if (attacking)
        {
            currentAttackFrames++;
            if (currentAttackFrames > attackFrames)
            {
                StopSwordAttack();
            }
        }
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

    public void SwordAttack()
    {
        if (!attacking)
        {
            currentAttackFrames = 0;
            sword.SetActive(true);
            attacking = true;
        }
    }

    public void StopSwordAttack()
    {
        sword.SetActive(false);
        attacking = false;
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
