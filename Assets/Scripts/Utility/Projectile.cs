using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum type{player, enemy}

    public type arrowType;
    public float speed;
    private Rigidbody2D rb;
    private float timer = 4f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Object.Destroy(gameObject);
        }
    }


    void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector3(speed, 0, 0));
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        switch(arrowType)
        {
            case type.player: if(col.gameObject.CompareTag("Enemy"))
                                col.gameObject.GetComponent<DamageManager>().takeDmg(10);
                              break;
            case type.enemy: if(col.gameObject.CompareTag("Player"))
                                col.gameObject.GetComponent<DamageManager>().takeDmg(10);
                             break;
        }

        if(!col.gameObject.CompareTag("Player") && !col.gameObject.CompareTag("PlayerArrow"))
            Object.Destroy(gameObject);
    }
}
