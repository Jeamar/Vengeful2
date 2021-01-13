using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [HideInInspector] public bool jumping = false;
    [HideInInspector] public bool crouchKey = false;
    [HideInInspector] public bool upKey = false;
    [HideInInspector] public bool altModeON = false;
    [HideInInspector] public bool altModeOFF = true;
    [HideInInspector] public bool attackKey = false;
    [HideInInspector] public bool blockKey = false;

    // Update is called once per frame
    void Update()
    {
        jumping = false;
        crouchKey = false;
        upKey = false;
        attackKey = false;
        blockKey = false;
        altModeON = false;
        altModeOFF = false;

        if(Input.GetKey(KeyCode.Space)) //jump key
            jumping = true;

        if(Input.GetKeyDown("s"))
            crouchKey = true;     //crouch / ballState key
            

        if(Input.GetKeyDown("w")) //standing up key
            upKey = true;

        if(Input.GetKeyDown(KeyCode.Mouse0)) //attack key
            attackKey = true;

        if(Input.GetKey(KeyCode.Mouse1)) //block key
            blockKey = true;

        if(Input.GetKey(KeyCode.LeftShift)) //alt mode, for shooting arrows
            altModeON = true;

        if(Input.GetKeyUp(KeyCode.LeftShift))
            altModeOFF = true;  
    }

    public float HorizontalAx()
    {
        return Input.GetAxis("Horizontal");
    }
}
