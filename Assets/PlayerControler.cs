using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private Rigidbody2D playerRB;
    private BoxCollider2D playerBC;
    private Vector2 InputSpeed;

    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float playerSpeed;
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float fallGravity;
    [SerializeField]
    private float extraGroundDetectionHeight;
    [SerializeField]
    private LayerMask platformsLayerM;


    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        playerBC = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Horizontal Movement
        playerRB.velocity = new Vector2(InputSpeed.x * playerSpeed * Time.deltaTime, playerRB.velocity.y);

        //Gravity
        if (!isGrounded()) {
            playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y + gravity * Time.deltaTime);
            if (playerRB.velocity.y < 0)
            {
                playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y + fallGravity * Time.deltaTime);
            }
            Debug.Log("Not Grounded");
        }
        else {
            Debug.Log("Grounded)");
        }
    }

    public void Jump()
    {
        if (isGrounded())
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpHeight);
        }
    }

    public void setInputSpeed(Vector2 InputSpeed)
    {
        this.InputSpeed = InputSpeed;
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(playerBC.bounds.center, Vector2.down, playerBC.bounds.extents.y + extraGroundDetectionHeight, platformsLayerM);
        return raycastHit.collider != null;
    }
}
