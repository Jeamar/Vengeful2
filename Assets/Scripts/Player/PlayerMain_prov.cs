using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMain_prov : UnitMain
{
    #region STATE VARIABLES
        public enum PlayerState{Idle, Jumping, Crouch, Ball, AltAttack, Dead}
        public PlayerState _plState, _prevState;

        public Transform standing, STbow, STfirePoint, STattackPoint;
        public Transform crouch, CRbow, CRfirePoint, CRattackPoint, ballAttackPoint;

        private bool idlingB = false, crouchingB = false, ballStateB = false;
        
    #endregion

    #region ATTACK VARIABLES
        //attackPoint is the transform where the attack radius is calculated
        //firePoint is the transform from where the arrows will get shot
        public Transform firePoint, attackPoint;
    #endregion
    
    #region SCRIPT MANAGERS
        public PlayerMove plMovement;
        public PlayerAttack plAttack;
        public PlayerInput plInput;
        public BowManager plBow;
        public PlayerAnimation plAnim;
    #endregion
    

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();

        Debug.Log("Player main script awake");
        idlingB = true;
        _plState = PlayerState.Idle; //The player will start in idle state

        plBow.bow = plBow.STbow;
        firePoint = STfirePoint;
        attackPoint = STattackPoint;

        //because the player starts in idle standing up, the positions of the bow and the transforms will be standing up
    }

    // Update is called once per frame
    void Update()
    {
        if(_plState != PlayerState.Dead)
        {
            keyTaker();
            stateManager();
        }
    }

    private void keyTaker()
    {
        if(plInput.crouchKey)
        {
            if(idlingB)
            {
                idlingB = false;
                crouchingB = true;
                ballStateB = false;
            }
            else if(crouchingB)
            {
                idlingB = false;
                crouchingB = false;
                ballStateB = true;
            }
        }

        if(plInput.upKey)
        {
            if(crouchingB)
            {
                idlingB = true;
                crouchingB = false;
                ballStateB = false;
            }
            else if(ballStateB)
            {
                idlingB = false;
                crouchingB = true;
                ballStateB = false;
            }
        }
    }

    private void stateManager()
    {
        //idle state
        if(idlingB && !plInput.jumping)
            _plState = PlayerState.Idle;

        //crouching state
        else if(crouchingB && !plInput.jumping)
            _plState = PlayerState.Crouch;

        //ball state
        else if(ballStateB)
            _plState = PlayerState.Ball;
            
        switch(_plState)
            { //this is probably place holder, changes the actual visuals from standing up to crouching of my object
            //also lets the player roll in ball state and returns him to z rotation 0 when going to crouch
                case PlayerState.Idle:  if(!standing.gameObject.activeSelf)
                                            {
                                                plAnim.crouch = false;
                                                plAnim.run = false;
                                            }
                                        /*if(plBow.bow.gameObject.activeSelf)
                                            {
                                                plBow.bow.gameObject.SetActive(false);
                                                plBow.bow = plBow.STbow;
                                                plBow.bow.gameObject.SetActive(true);
                                            }
                                        else
                                            plBow.bow = plBow.STbow;
                                        firePoint = STfirePoint;
                                        attackPoint = STattackPoint;*/
                                        break;
                case PlayerState.Crouch:gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                                        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                                        /*if(!crouch.gameObject.activeSelf)
                                            {
                                                standing.gameObject.SetActive(false);
                                                crouch.gameObject.SetActive(true);
                                            }
                                        if(plBow.bow.gameObject.activeSelf)
                                        {
                                            plBow.bow.gameObject.SetActive(false);
                                            plBow.bow = plBow.CRbow;
                                            plBow.bow.gameObject.SetActive(true);
                                        }
                                        else
                                            plBow.bow = plBow.CRbow;
                                        firePoint = CRfirePoint;
                                        attackPoint = CRattackPoint;*/
                                        plAnim.crouch = true;
                                        break;
                case PlayerState.Ball:  rb.constraints = RigidbodyConstraints2D.None;
                                        break;
            }


        //Enters alternate mode, from wich you can fire arrows
        if(_plState != PlayerState.Ball)
        {
            if(plInput.altModeON)
            {
                plBow.manageArm(true); //this function manages the angle of the arm to fire arrows
                //the boolean allows it to rotate

                if(_plState != PlayerState.AltAttack && _plState != PlayerState.Jumping)
                    _prevState = _plState;
                _plState = PlayerState.AltAttack;
            }
            else if(plInput.altModeOFF)
            {
                _plState = _prevState;
                plBow.manageArm(false);
            }
        }

        if(currentHP <= 0)
        {
            currentHP = 0;
            _plState = PlayerState.Dead;
        }
    }
}
