using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : UnitAttack
{
    public float ballAttackRange;
    public LayerMask enemyLayers;
    public GameObject arrow;
    private float shotTime = 0.25f;

    public PlayerMain playerM;
    public GrapplingHook grapHook;

    // Update is called once per frame
    protected override void Update()
    {
        shotTime -= Time.deltaTime; //a timer for the arrow shot
        if(shotTime <= 0)
            shotTime = 0;
        attack();
        defensive();

        if(/*grapHook.grappled &&*/ playerM.plInput.altModeOFF)
            grapHook.Detach();
    }

    void LateUpdate()
    {
        if(playerM._plState == PlayerMain.PlayerState.AltAttack && playerM.plInput.blockKey && !grapHook.grappled)
            grapHook.grapple();
    }

    protected override void attack()
    {
        //Manages the attack type, if the player isn't blocking, he can attack
        if(playerM.plInput.attackKey && !playerM.plInput.blockKey)
        {
            switch(playerM._plState)
            {
                case PlayerMain.PlayerState.AltAttack: if(shotTime <= 0)
                                                       {
                                                           //instantiates an arrow
                                                           //wich will go in the direction the arm is pointing
                                                           GameObject arrowClone = Instantiate(arrow);
                                                           arrowClone.transform.position = playerM.firePoint.position;
                                                           arrowClone.transform.rotation = playerM.plBow.bow.rotation;
                                                           shotTime = 0.5f;
                                                       }
                                                       break;
                default: meleeAttack();
                         break;
            }
        }
    }


    private void meleeAttack()
    {
        Collider2D[] hitEnemies;
        float hit;

        //checks if the player is standing up, crouching or in ball mode
        //each one has a different range of attack, so this calculates which one to use
        if(playerM._plState == PlayerMain.PlayerState.Idle || playerM._plState == PlayerMain.PlayerState.Crouch)
        {
            melee = Physics2D.OverlapCircleAll(playerM.attackPoint.position, attackRange, enemyLayers);
            dmg = 20;
        }
        else if(playerM._plState == PlayerMain.PlayerState.Ball)
        {
            melee = Physics2D.OverlapCircleAll(playerM.ballAttackPoint.position, ballAttackRange, enemyLayers);
            dmg = 30;
        }

        //applies the range
        hitEnemies = melee;
        hit = dmg;

        //attacks every enemy in range, all of this is from a Brackeys tutorial
        foreach(Collider2D enemy in hitEnemies)
            enemy.gameObject.GetComponent<DamageManager>().takeDmg(hit);
    }

    void OnDrawGizmosSelected()
    {
        //this lets me see the ranges in the scene
        Gizmos.DrawWireSphere(playerM.STattackPoint.position, attackRange);

        Gizmos.DrawWireSphere(playerM.CRattackPoint.position, attackRange);

        Gizmos.DrawWireSphere(playerM.ballAttackPoint.position, ballAttackRange);
    }

    private void defensive()
    {
        if(playerM.plInput.blockKey)
        {
            if(playerM._plState != PlayerMain.PlayerState.AltAttack)
                Debug.Log("Block");
            /*else
            {
                grapHook.grapple();
            }*/
        }
    }
}
