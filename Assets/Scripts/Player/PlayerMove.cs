using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : UnitMove
{
    [HideInInspector] public PlayerMain playerM;

    #region MOVEMENT VARIABLES
        public float coyoteTime, lastTimeGrounded;
        private float horizontalMove, jumpTimeCounter = 0, jumpTime = 0.35f;
    #endregion

    void Start()
    {
        playerM = GetComponent<PlayerMain>();
    }

    protected override void Update()
    {
        base.Update();

        if(!isGrounded)
            lastTimeGrounded = 0;
        if(isGrounded)
            lastTimeGrounded = Time.time;

        /*if(playerM._plState != PlayerMain.PlayerState.Dead)
        {
            jump();
            move();
        }*/
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(playerM._plState != PlayerMain.PlayerState.Dead)
        {
            jump();
            move();
        }
    }

    private void jump()
    {
        //Jumps if player is standing on a "jumpable surface" (ground layer), vertical force is applied to the player
        //The player state changes to jumping until the jump ends
        if((isGrounded || Time.time - lastTimeGrounded <= coyoteTime)
             && playerM.plInput.jumping && playerM._plState != PlayerMain.PlayerState.Ball && playerM._plState != PlayerMain.PlayerState.AltAttack)
        {
            playerM._plState = PlayerMain.PlayerState.Jumping;
            jumpTimeCounter = jumpTime;
            playerM.rb.AddForce(Vector2.up * jumpForce);
            //flastTimeGrounded = 0;

            if(!playerM.standing.gameObject.activeSelf)
            {
                playerM.crouch.gameObject.SetActive(false);
                playerM.standing.gameObject.SetActive(true);
                //if the player was crouching (not in ball mode), stands up
            }
        }


        //If you keep pressing the jump key for some time, it should jump higher based on that time
        //doesn't work for some reason
        if(playerM.plInput.jumping && playerM._plState == PlayerMain.PlayerState.Jumping)
        {
            if(jumpTimeCounter > 0)
            {
                playerM.rb.AddForce(Vector2.up * jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                playerM._plState = PlayerMain.PlayerState.Idle;
                //if the time runs out, stops the jump and goes to idle
        }

        //if you release jump key, stops the jump and goes to idle
        if(!playerM.plInput.jumping)
        {
            if(playerM._plState == PlayerMain.PlayerState.Jumping || playerM._plState == PlayerMain.PlayerState.Idle)
                playerM._plState = PlayerMain.PlayerState.Idle;
        }
    }

    private void move()
    {
        //if the player is in idle, in ball mode or in alt mode standing up
        if((playerM._plState == PlayerMain.PlayerState.Idle || playerM._plState == PlayerMain.PlayerState.Ball) 
            || (playerM._prevState == PlayerMain.PlayerState.Idle && playerM._plState == PlayerMain.PlayerState.Idle))
        {
            horizontalMove = playerM.plInput.HorizontalAx(); //gets the axis for the movement
        }

        if(playerM._plState != PlayerMain.PlayerState.Crouch &&  playerM._plState != PlayerMain.PlayerState.AltAttack)
        {
            playerM.rb.AddForce(new Vector3(horizontalMove * speed * Time.deltaTime, 0, 0));
            //playerM.plAnim.run = true;
        }
            //applies the force if the player isn't crouching, this solved an error I had
            //where if you pressed the crouch key while moving the force would be applied forever
    }
}
