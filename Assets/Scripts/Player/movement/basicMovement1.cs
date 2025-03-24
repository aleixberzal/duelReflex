using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicMovement1 : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public GameObject render;

    [Header("Movimiento")]

    private float horizontalMovement = 0f;

    /*public variables to edit from inspector*/

    [SerializeField] public float movementSpeed = 10f;
    [SerializeField] public float forceController = 30f;
    [SerializeField] public float airForce = 2f;
    [SerializeField] public bool touchingFloor = true;

    /*To turn the sprite around*/
    private bool lookingRight = true;
    
    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        /*It gets the player's input (1 to go right, -1 to go left)*/
        horizontalMovement = Input.GetAxisRaw("HorizontalB");
    }

    private void FixedUpdate()
    {
        if (horizontalMovement != 0)
        {
            ApplyForce(horizontalMovement);
        }else
        {
            BreakMovement();
        }
    }

    private void ApplyForce(float direction)
    {

        float actualSpeed = rb2D.velocity.x;
        float targetSpeed = direction * movementSpeed;
        float speedDiff = targetSpeed - actualSpeed;

        Vector2 force;
 
        force = new Vector2(speedDiff * forceController, 0);
 
        rb2D.AddForce(force, ForceMode2D.Force);


    }

    private void BreakMovement()
    {
        float horizontalSpeed = rb2D.velocity.x;

        rb2D.velocity = new Vector2(0f, rb2D.velocity.y);
    }

    private void Turn()
    {
        lookingRight = !lookingRight;
        Vector3 scale = render.transform.localScale;
        scale.x *= -1;
        render.transform.localScale = scale;
    }
}
