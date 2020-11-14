using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerControler PlayerControler;
    // Start is called before the first frame update
    void Start()
    {
        PlayerControler = GetComponent<PlayerControler>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerControler.setInputSpeed(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized);
        if (Input.GetButtonDown("Jump"))
        {
            PlayerControler.Jump();
        }
        if (Input.GetButtonUp("Jump"))
        {
            //PlayerControler.StopJump();
        }
        if (Input.GetButtonDown("Run"))
        {
            //PlayerControler.Run();
        }
        if (Input.GetButtonUp("Run"))
        {
            //PlayerControler.StopRun();
        }
        if (Input.GetButtonDown("Interact"))
        {
        }
        if (Input.GetButtonDown("Hit"))
        {
        }
        if (Input.GetButtonDown("Menu"))
        {
            //    SceneManager.LoadScene(0);
        }
    }
}
