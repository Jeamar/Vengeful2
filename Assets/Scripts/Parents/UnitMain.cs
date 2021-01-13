using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMain : MonoBehaviour
{
    public float currentHP;
    
    private float maxHP = 100;
    [HideInInspector] public Rigidbody2D rb;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHP = maxHP;
    }
}
