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
        PlayerControler.SetInputSpeed(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized);
        if (Input.GetButtonDown("Interact"))
        {
            PlayerControler.Interact();
        }
    /*        if (Input.GetButtonUp("Jump"))
            {
            }
            if (Input.GetButtonDown("Run"))
            {
            }
            if (Input.GetButtonUp("Run"))
            {
            }
            if (Input.GetButtonDown("Interact"))
            {
            }*/
        if (Input.GetButtonDown("Fire1"))
        {
            PlayerControler.SwordAttack();
            
        }
        if (Input.GetButtonDown("Menu"))
        {
        }
        if (Input.GetButtonDown("Fire2"))
        {
            GameManager.NotifyTroubleDone(10);
        }
    }
}
