using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : UnitMove
{
    public float direction, idleTimer, chaseSpeed, time = 4;

    public EnemyMain enMain;
    // Start is called before the first frame update
    void Start()
    {
        idleTimer = time;
    }

    protected override void Update()
    {
        base.Update();

        idleTimer -= Time.deltaTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        if(enMain.enAttack.canMove)
        {
            if(enMain.isChasing)
                chase();
            else
                idlePatrol();
        }
    }

    private void idlePatrol()
    {
        if(idleTimer <= 0)
        {
            float force = Random.Range(-1, 1);
            //direction = force >= 0 ? 1 : -1;
            if(force >= 0)
                direction = 1;
            else
                direction = -1;

            enMain.rb.AddForce(new Vector3(direction * speed * Time.deltaTime, 0, 0));

            idleTimer = time;
        }
    }

    private void chase()
    {
        float dir = transform.position.x < enMain.player.position.x ? 1 : -1;

        enMain.rb.AddForce(new Vector3(dir * (speed / chaseSpeed) * Time.deltaTime, 0, 0));
    }
}
