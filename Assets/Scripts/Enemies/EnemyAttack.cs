using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : UnitAttack
{
    public LayerMask playerLayer;
    public Transform attackPoint;
    private float moveTime = 2f, moveTimer;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        moveTimer = moveTime;
        canMove = true;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(canMove)
            attack();
        else
            manageTimer();
    }

    protected override void attack()
    {
        melee = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D obj in melee)
        {
            if(obj.gameObject.CompareTag("Player"))
            {
                obj.gameObject.GetComponent<DamageManager>().takeDmg(dmg);
                canMove = false;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;
            
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void manageTimer()
    {
        if(moveTimer > 0)
            moveTimer -= Time.deltaTime;
        else
        {
            canMove = true;
            moveTimer = moveTime;
        }
    }
}
