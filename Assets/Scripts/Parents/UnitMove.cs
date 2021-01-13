using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    public float speed, jumpForce, checkRadius = 0.1f;
    public bool isGrounded;
    public Transform feetPos;
    public LayerMask whatIsGround;

    // Update is called once per frame
    protected virtual void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        //will verify if the unit is on ground
    }
}
