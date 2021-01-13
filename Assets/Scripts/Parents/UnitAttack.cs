using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttack : MonoBehaviour
{
    public float attackRange, dmg = 20;
    protected Collider2D[] melee;

    protected virtual void Update()
    {
        attack(); //abstract function is called, the function will be implemented in the child
    }

    protected virtual void attack(){}

}
