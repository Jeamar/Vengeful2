using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : UnitMain
{
    public float aggroRange, aggroTime, time = 4;
    public bool isChasing;
    public Transform castPoint, player;
    public LayerMask playerLayer;

    [HideInInspector] public EnemyMove enMove;
    [HideInInspector] public EnemyAttack enAttack;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        enMove = GetComponent<EnemyMove>();
        enAttack = GetComponent<EnemyAttack>();
    }

    // Update is called once per frame
    void Update()
    {
        if((aggroRange > 0 && enMove.direction < 0) || (aggroRange < 0 && enMove.direction > 0))
            aggroRange *= -1;
        
        manageAggro(canSeePlayer());
    }

    protected virtual bool canSeePlayer()
    {
        bool check = false;

        RaycastHit2D hit = Physics2D.Raycast(castPoint.position, Vector3.right, /*(int)*/ aggroRange, playerLayer);
        Debug.DrawRay(castPoint.position, Vector3.right * aggroRange, Color.green);

        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                check = true;
                Debug.DrawRay(castPoint.position, Vector3.right * aggroRange, Color.red);
            }
            else
            {
                check = false;
                Debug.DrawRay(castPoint.position, Vector3.right * aggroRange, Color.blue);
            }
        }

        return check;
    }

    private void manageAggro(bool aggro)
    {
        if(aggro)
        {
            isChasing = true;
            aggroTime = time;
        }
        else if(!aggro && aggroTime > 0)
        {
            isChasing = true;
            aggroTime -= Time.deltaTime;
        }
        else if(!aggro && aggroTime <= 0)
        {
            isChasing = false;
            aggroTime = 0;
        }
    }
}
